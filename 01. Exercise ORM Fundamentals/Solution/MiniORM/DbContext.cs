﻿using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MiniORM;

public abstract class DbContext
{
    private readonly DatabaseConnection _connection;
    private readonly Dictionary<Type, PropertyInfo> _dbSetProperties;

    internal static readonly Type[] AllowedSqlTypes =
    {
        typeof(string),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(decimal),
        typeof(bool),
        typeof(DateTime)
    };

    protected DbContext(string connectionString)
    {
        _connection = new DatabaseConnection(connectionString);
        _dbSetProperties = DiscoverDbSets();
        using (new ConnectionManager(_connection))
        {
            InitializeDbSets();
        }

        MapAllRelations();
    }

    public void SaveChanges()
    {
        var dbSets = _dbSetProperties.Select(pi => pi.Value.GetValue(this)).ToArray();

        foreach (IEnumerable<object> dbSet in dbSets)
        {
            var invalidEntities = dbSet.Where(e => !IsObjectValid(e)).ToArray();

            if (invalidEntities.Any())
                throw new InvalidOperationException($"{invalidEntities.Length} Invalid Entities Found in {dbSet.GetType().Name}!");
        }

        using (new ConnectionManager(_connection))
        {
            using (var transaction = _connection.StartTransaction())
            {
                foreach (IEnumerable dbSet in dbSets)
                {
                    try
                    {
                        var persistMethod = typeof(DbContext)
                            .GetMethod(nameof(Persist), BindingFlags.NonPublic | BindingFlags.Instance)!
                            .MakeGenericMethod(dbSet.GetType().GetGenericArguments().First());

                        try
                        {
                            persistMethod.Invoke(this, new object[] { dbSet });
                        }
                        catch (TargetInvocationException e)
                        {
                            throw e.InnerException;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Rollback!!!");
                        transaction.Rollback();
                        throw;
                    }
                }

                transaction.Commit();
            }
        }
    }

    private void Persist<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
    {
        var tableName = GetTableName(typeof(TEntity));
        var columns = _connection.FetchColumnNames(tableName).ToArray();

        if (dbSet.ChangeTracker.Added.Any())
            _connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columns);

        var modifiedEntities = dbSet.ChangeTracker.GetModifiedEntities(dbSet).ToArray();
        if (modifiedEntities.Any()) _connection.UpdateEntities(modifiedEntities, tableName, columns);

        if (dbSet.ChangeTracker.Removed.Any()) _connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columns);
    }

    private void InitializeDbSets()
    {
        foreach (var dbSet in _dbSetProperties)
        {
            var dbSetType = dbSet.Key;
            var dbSetProperty = dbSet.Value;

            var populateDbSetGeneric = typeof(DbContext)
                .GetMethod(nameof(PopulateDbSet), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(dbSetType);
            populateDbSetGeneric.Invoke(this, new object[] { dbSetProperty });
        }
    }

    private void PopulateDbSet<TEntity>(PropertyInfo dbSet)
        where TEntity : class, new()
    {
        var entities = LoadTableEntities<TEntity>();
        var dbSetInstance = new DbSet<TEntity>(entities);
        ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
    }

    private void MapAllRelations()
    {
        foreach (var dbSetProperty in _dbSetProperties)
        {
            var dbSetType = dbSetProperty.Key;
            var mapRelationsGeneric = typeof(DbContext)
                .GetMethod(nameof(MapRelations), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(dbSetType);
            var dbSet = dbSetProperty.Value.GetValue(this);
            mapRelationsGeneric.Invoke(this, new[] { dbSet });
        }
    }

    private void MapRelations<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity);
        MapNavigationProperties(dbSet);

        var collections = entityType.GetProperties()
            .Where(pi => pi.PropertyType.IsGenericType &&
                         pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToArray();

        foreach (var collection in collections)
        {
            var collectionType = collection.PropertyType.GenericTypeArguments.First();
            var mapCollectionMethod = typeof(DbContext)
                .GetMethod(nameof(MapCollection), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(entityType, collectionType);
            mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
        }
    }

    private void MapCollection<TDbSet, TCollection>(DbSet<TDbSet> dbSet, PropertyInfo collectionProperty)
        where TDbSet : class, new()
        where TCollection : class, new()
    {
        var entityType = typeof(TDbSet);
        var collectionType = typeof(TCollection);

        var primaryKeys = collectionType.GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray();

        var primaryKey = primaryKeys.First();
        var foreignKey = entityType.GetProperties()
            .First(pi => pi.HasAttribute<KeyAttribute>());

        var isManyToMany = primaryKeys.Length >= 2;
        if (isManyToMany)
        {
            primaryKey = collectionType.GetProperties()
                .First(pi => collectionType
                    .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name)
                                   .PropertyType == entityType);
        }

        var navigationDbSet = (DbSet<TCollection>)_dbSetProperties[collectionType].GetValue(this);
        foreach (var entity in dbSet)
        {
            var primaryKeyValue = foreignKey.GetValue(entity);
            var navigationEntities = navigationDbSet
                .Where(navigationEntity => primaryKey.GetValue(navigationEntity).Equals(primaryKeyValue))
                .ToArray();

            ReflectionHelper.ReplaceBackingField(entity, collectionProperty.Name, navigationEntities);
        }
    }

    private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
    {
        var entityType = typeof(TEntity);
        var foreignKeys = entityType.GetProperties()
            .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
            .ToArray();

        foreach (var foreignKey in foreignKeys)
        {
            var navigationPropertyName = foreignKey.GetCustomAttribute<ForeignKeyAttribute>().Name;
            var navigationProperty = entityType.GetProperty(navigationPropertyName);

            var navigationDbSet = _dbSetProperties[navigationProperty.PropertyType].GetValue(this);
            var navigationPrimaryKey = navigationProperty.PropertyType.GetProperties()
                .First(pi => pi.HasAttribute<KeyAttribute>());

            foreach (var entity in dbSet)
            {
                var foreignKeyValue = foreignKey.GetValue(entity);
                var navigationPropertyValue = ((IEnumerable<object>)navigationDbSet)
                    .First(currentNavigationProperty =>
                                               navigationPrimaryKey.GetValue(currentNavigationProperty).Equals(foreignKeyValue));

                navigationProperty.SetValue(entity, navigationPropertyValue);
            }
        }
    }

    private static bool IsObjectValid(object e)
    {
        var validationContext = new ValidationContext(e);
        var validationErrors = new List<ValidationResult>();

        var validationResult = Validator.TryValidateObject(e, validationContext, validationErrors, true);
        return validationResult;
    }

    private IEnumerable<TEntity> LoadTableEntities<TEntity>()
        where TEntity : class
    {
        var table = typeof(TEntity);
        var columns = GetEntityColumnNames(table).ToArray();
        var tableName = GetTableName(table);

        var fetchedRows = _connection.FetchResultSet<TEntity>(tableName, columns).ToArray();
        return fetchedRows;
    }

    private string GetTableName(Type tableType)
    {
        var tableName = ((TableAttribute)Attribute.GetCustomAttribute(tableType, typeof(TableAttribute)))?.Name ??
                        _dbSetProperties[tableType].Name;

        return tableName;
    }

    private Dictionary<Type, PropertyInfo> DiscoverDbSets()
    {
        var dbSets = GetType().GetProperties()
            .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);

        return dbSets;
    }

    private string[] GetEntityColumnNames(Type table)
    {
        var tableName = GetTableName(table);
        var dbColumns = _connection.FetchColumnNames(tableName);

        var columns = table.GetProperties()
            .Where(pi => dbColumns.Contains(pi.Name) && !pi.HasAttribute<NotMappedAttribute>())
            .Select(pi => pi.Name)
            .ToArray();

        return columns;
    }
}