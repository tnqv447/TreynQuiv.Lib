using System.Linq.Expressions;

namespace TreynQuiv.Lib.EntityFrameworkCore;

public interface IAsyncRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates);
    Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<IList<TEntity>> ListAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
