using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MiniORM;

public class ChangeTracker<T> where T : class, new()
{
    private readonly List<T> _entities;
    private readonly List<T> _added;
    private readonly List<T> _removed;

    public ChangeTracker(IEnumerable<T> entities)
    {
        _added = new List<T>();
        _removed = new List<T>();
        _entities = CloneEntities(entities);
    }

    private List<T> CloneEntities(IEnumerable<T> entities)
    {
        var clonedEntities = new List<T>();

        var propertiesToClone = typeof(T).GetProperties()
            .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
            .ToArray();

        foreach (var entity in entities)
        {
            var clonedEntity = Activator.CreateInstance<T>();

            foreach (var property in propertiesToClone)
            {
                var value = property.GetValue(entity);
                property.SetValue(clonedEntity, value);
            }

            clonedEntities.Add(clonedEntity);
        }

        return clonedEntities;
    }

    public IReadOnlyCollection<T> AllEntities => _entities.AsReadOnly();
    public IReadOnlyCollection<T> Added => _added.AsReadOnly();
    public IReadOnlyCollection<T> Removed => _removed.AsReadOnly();

    public void Add(T entity) => _added.Add(entity);
    public void Remove(T entity) => _removed.Add(entity);

    public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
    {
        var modifiedEntities = new List<T>();

        var primaryKeys = typeof(T).GetProperties()
            .Where(pi => pi.HasAttribute<KeyAttribute>())
            .ToArray();

        foreach (var proxyEntity in AllEntities)
        {
            var entityKeyValue = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();
            var entity = dbSet.Entities
                .Single(e => GetPrimaryKeyValues(primaryKeys, e)
                    .SequenceEqual(entityKeyValue));

            var isModified = IsModified(proxyEntity, entity);
            if (isModified) modifiedEntities.Add(entity);
        }

        return modifiedEntities;
    }

    private static bool IsModified(T entity, T proxyEntity)
    {
        var monitoredProperties = typeof(T).GetProperties()
            .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType));

        var modifiedProperties = monitoredProperties
            .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)))
            .ToArray();

        var isModified = modifiedProperties.Any();
        return isModified;
    }

    private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        => primaryKeys.Select(pk => pk.GetValue(entity))!;
}