using System.Linq.Expressions;
using TreynQuiv.Lib.Components;

namespace TreynQuiv.Lib.Database;

/// <summary>
/// A base generic repository interface with asynchronous methods.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAsyncRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Return the first <typeparamref name="TEntity"/> found after querying the collection.
    /// </summary>
    /// <returns>The found <typeparamref name="TEntity"/>.</returns>
    Task<TEntity> FirstAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Return the first <typeparamref name="TEntity"/> found after querying the collection. If the result is empty, return <see langword="null"/>.
    /// </summary>
    /// <returns>The found <typeparamref name="TEntity"/> or <see langword="null"/>.</returns>
    Task<TEntity?> FirstOrDefaultAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Return a <see cref="IReadOnlyList{TEntity}"/> found after querying the collection.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyList{TEntity}"/>.</returns>
    Task<IReadOnlyList<TEntity>> ListAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Return a <see cref="IReadOnlyList{TEntity}"/> after querying the collection and then applying <see cref="PagingOptions"/>.
    /// </summary>
    /// <returns>A paginated <see cref="IReadOnlyList{TEntity}"/>, total records count and total pages count.</returns>
    Task<(IReadOnlyList<TEntity> records, long totalRecords, long totalPages)> ListAsync(PagingOptions pagingOptions, params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Counts the found <typeparamref name="TEntity"/> after querying the collection.
    /// </summary>
    /// <returns>Count value as <see cref="int"/>.</returns>
    Task<int> CountAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Counts the found <typeparamref name="TEntity"/> after querying the collection.
    /// </summary>
    /// <returns>Count value as <see cref="long"/>.</returns>
    Task<long> LongCountAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Add an <typeparamref name="TEntity"/> to the collection.
    /// </summary>
    /// <returns>The passed <paramref name="entity"/>.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Update an <typeparamref name="TEntity"/> in the collection.
    /// </summary>
    /// <returns>The passed <paramref name="entity"/>.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Remove an <typeparamref name="TEntity"/> from the collection.
    /// </summary>
    Task DeleteAsync(TEntity entity);
}
