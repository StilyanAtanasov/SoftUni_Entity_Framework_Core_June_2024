using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eventmi.Data.Common;

public interface IRepository : IDisposable
{
    IQueryable<T> AllReadonly<T>() where T : class;

    Task<T?> GetByIdAsync<T>(object id) where T : class;

    Task<T?> FindByExpressionAsync<T>(Expression<Func<T, bool>> expression) where T : class;

    public Task<T?> FindByExpressionAsync<T>(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes) where T : class;

    Task AddAsync<T>(T entity) where T : class;

    void Update<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    Task<int> SaveChangesAsync();
}