using System.Linq.Expressions;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// Extends <see cref="IAsyncRepository{}"/> with <see langword="EFCore"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEFCoreAsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Query database through all <paramref name="predicates"/>.
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns>A <see cref="EFCoreQuery{}"/>.</returns>
    EFCoreQuery<TEntity> Query(params Expression<Func<TEntity, bool>>[] predicates);
}
