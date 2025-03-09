using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eventmi.Data.Common;

public class EfRepository : IRepository
{
    private readonly DbContext _context;

    public EfRepository(EventmiDbContext context) => _context = context;

    public void Dispose() => _context.Dispose();

    protected DbSet<T> DbSet<T>() where T : class => _context.Set<T>();

    public IQueryable<T> AllReadonly<T>() where T : class => DbSet<T>().AsNoTracking();

    public async Task<T?> GetByIdAsync<T>(object id) where T : class => await DbSet<T>().FindAsync(id);

    public async Task<T?> FindByExpressionAsync<T>(Expression<Func<T, bool>> expression) where T : class =>
        await DbSet<T>().FirstOrDefaultAsync(expression);

    public async Task<T?> FindByExpressionAsync<T>(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes) where T : class
    {
        IQueryable<T> query = DbSet<T>();
        foreach (Expression<Func<T, object>> include in includes) query = query.Include(include);

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task AddAsync<T>(T entity) where T : class => await DbSet<T>().AddAsync(entity);

    public void Update<T>(T entity) where T : class => DbSet<T>().Update(entity);

    public void Delete<T>(T entity) where T : class => DbSet<T>().Remove(entity);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}