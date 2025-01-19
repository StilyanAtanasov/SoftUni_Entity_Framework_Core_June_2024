using System.Collections;

namespace MiniORM;

public class DbSet<TEntity> : ICollection<TEntity> where TEntity : class, new()
{
    internal ChangeTracker<TEntity> ChangeTracker { get; set; }
    internal IList<TEntity> Entities { get; set; }

    internal DbSet(IEnumerable<TEntity> entities)
    {
        Entities = entities.ToList();
        ChangeTracker = new ChangeTracker<TEntity>(Entities);
    }

    public void Add(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null!");

        Entities.Add(entity);
        ChangeTracker.Add(entity);
    }

    public void Clear()
    {
        while (Entities.Any())
        {
            var entity = Entities.First();
            Remove(entity);
        }
    }

    public bool Contains(TEntity item) => Entities.Contains(item);

    public void CopyTo(TEntity[] array, int arrayIndex) => Entities.CopyTo(array, arrayIndex);

    public int Count => Entities.Count;

    public bool IsReadOnly => Entities.IsReadOnly;

    public bool Remove(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null!");

        var removedSuccessfully = Entities.Remove(entity);
        if (removedSuccessfully) ChangeTracker.Remove(entity);

        return removedSuccessfully;
    }

    public IEnumerator<TEntity> GetEnumerator() => Entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities.ToArray()) Remove(entity);
    }
}