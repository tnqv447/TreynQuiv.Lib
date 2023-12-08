using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TreynQuiv.Lib.EntityFrameworkCore;

public class AsyncRepository<TEntity>(DbContext context) : IAsyncRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> SingleAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = predicate is null ? _dbSet : _dbSet.Where(predicate);
        return await query.ToListAsync();
    }

    public async Task<IList<TEntity>> ListAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates)
    {
        var query = _dbSet.AsQueryable();
        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = await Task.FromResult(_context.Attach(entity));
        entry.State = EntityState.Modified;
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        await Task.Run(() => _dbSet.Remove(entity));
    }
}
