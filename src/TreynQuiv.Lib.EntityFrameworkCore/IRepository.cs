using System.Linq.Expressions;

namespace TreynQuiv.Lib.EntityFrameworkCore;

public interface IRepository<TEntity> where TEntity : IEntity
{
    TEntity? Single(Expression<Func<TEntity, bool>> predicate);
    TEntity? Single(IEnumerable<Expression<Func<TEntity, bool>>> predicates);
    IList<TEntity> List(Expression<Func<TEntity, bool>>? predicate = null);
    IList<TEntity> List(IEnumerable<Expression<Func<TEntity, bool>>> predicates);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}
