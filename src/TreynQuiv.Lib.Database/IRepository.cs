using System.Linq.Expressions;

namespace TreynQuiv.Lib.Database;

/// <summary>
/// A base generic repository interface.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Return the first <typeparamref name="TEntity"/> found after querying the collection.
    /// </summary>
    /// <returns>The found <typeparamref name="TEntity"/>.</returns>
    TEntity First(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Return the first <typeparamref name="TEntity"/> found after querying the collection. If the result is empty, return <see langword="null"/>.
    /// </summary>
    /// <returns>The found <typeparamref name="TEntity"/> or <see langword="null"/>.</returns>
    TEntity? FirstOrDefault(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Return a <see cref="IReadOnlyList{TEntity}"/> found after querying the collection.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyList{TEntity}"/>.</returns>
    IReadOnlyList<TEntity> List(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// Add an <typeparamref name="TEntity"/> to the collection.
    /// </summary>
    /// <returns>The passed <paramref name="entity"/>.</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Update an <typeparamref name="TEntity"/> in the collection.
    /// </summary>
    /// <returns>The passed <paramref name="entity"/>.</returns>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Remove an <typeparamref name="TEntity"/> from the collection.
    /// </summary>
    void Remove(TEntity entity);
}
