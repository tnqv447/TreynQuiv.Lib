using System.Linq.Expressions;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// Extends <see cref="IAsyncRepository{}"/> with <see langword="EFCore"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEFCoreAsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Return a <see cref="IReadOnlyList{TEntity}"/> found after querying the collection
    /// that also querying navigation properties.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyList{TEntity}"/>.</returns>
    Task<IReadOnlyList<TEntity>> ListAsync(IEnumerable<Expression<Func<TEntity, dynamic>>> includes,
                                params Expression<Func<TEntity, bool>>[] predicates);
}
