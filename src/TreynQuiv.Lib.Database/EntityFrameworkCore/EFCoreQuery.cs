using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TreynQuiv.Lib.Components;
using TreynQuiv.Lib.Database.EntityFrameworkCore.Extensions;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

public record class EFCoreQuery<TEntity> where TEntity : class, IEntity
{
    protected internal IQueryable<TEntity> _query;
    public IReadOnlyList<TEntity> List()
    {
        return _query.ToList().AsReadOnly();
    }

    public IReadOnlyList<TEntity> List(PagingOptions pagingOptions, out long totalRecords, out long totalPages, params Expression<Func<TEntity, bool>>[] predicates)
    {
        return _query.Paging(pagingOptions, out totalRecords, out totalPages).ToList().AsReadOnly();
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync()
    {
        return (await _query.ToListAsync()).AsReadOnly();
    }

    public async Task<(IReadOnlyList<TEntity> records, long totalRecords, long totalPages)> ListAsync(PagingOptions pagingOptions)
    {
        var records = await _query.Paging(pagingOptions, out var totalRecords, out var totalPages).ToListAsync();
        return (records.AsReadOnly(), totalRecords, totalPages);
    }

    internal EFCoreQuery(IQueryable<TEntity> query) { _query = query; }

    /// <summary>
    /// Specifies related entities to include in the query results.
    /// The navigation property to be included is specified starting
    /// with the type of entity being queried (<typeparamref name="TEntity"/>). Further navigation
    /// properties to be included can be appended, separated by the '.' character.
    /// <para>See Loading related entities for more information and examples.</para>
    /// </summary>
    /// <returns>A new query with the related data included.</returns>
    public EFCoreQuery<TEntity> Include<TProperty>(string propertyName)
    {
        _query = _query.Include(propertyName);
        return this;
    }

    /// <summary>
    /// Specifies related entities to include in the query results.
    /// The navigation property to be included is specified starting
    /// with the type of entity being queried (<typeparamref name="TEntity"/>).
    /// If you wish to include additional types based on the navigation properties of the
    /// type being included, then chain a call to <see cref="EFCoreIncludedQuery{,}.ThenInclude{}"/>
    /// <para>See Loading related entities for more information and examples.</para>
    /// </summary>
    /// <returns>A new query with the related data included.</returns>
    public EFCoreIncludedQuery<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> includeExpression)
    {
        return new(_query.Include(includeExpression));
    }

    /// <summary>
    /// Sorts the elements of a sequence in ascending order
    /// by using a specified comparer or <see cref="Comparer{}.Default"/>.
    /// </summary>
    /// <returns>An <see cref="EFCoreOrderedQuery{}"/> whose elements are sorted according to a key.</returns>
    public EFCoreOrderedQuery<TEntity> OrderBy<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        return new(_query.OrderBy(keySelector, comparer ?? Comparer<TProperty>.Default));
    }

    /// <summary>
    /// Sorts the elements of a sequence in descending order
    /// by using a specified comparer or <see cref="Comparer{}.Default"/>.
    /// </summary>
    /// <returns>An <see cref="EFCoreOrderedQuery{}"/> whose elements are sorted in descending order according to a key.</returns>
    public EFCoreOrderedQuery<TEntity> OrderByDescending<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        return new(_query.OrderByDescending(keySelector, comparer ?? Comparer<TProperty>.Default));
    }
}

public sealed record class EFCoreIncludedQuery<TEntity, TProperty> : EFCoreQuery<TEntity> where TEntity : class, IEntity
{
    private readonly IIncludableQueryable<TEntity, TProperty> _includeQuery;
    internal EFCoreIncludedQuery(IIncludableQueryable<TEntity, TProperty> query) : base(query)
    {
        _includeQuery = query;
    }

    /// <summary>
    /// Specifies additional related data to be further included
    /// based on a related type that was just included.
    /// <para>See Loading related entities for more information and examples.</para>
    /// </summary>
    /// <param name="includeExpression"></param>
    /// <typeparam name="TPropertyMember"></typeparam>
    /// <returns>A new query with the related data included.</returns>
    public EFCoreIncludedQuery<TEntity, TPropertyMember> ThenInclude<TPropertyMember>(Expression<Func<TProperty, TPropertyMember>> includeExpression)
    {
        return new(_includeQuery.ThenInclude(includeExpression));
    }
}

public sealed record class EFCoreOrderedQuery<TEntity> : EFCoreQuery<TEntity> where TEntity : class, IEntity
{
    private IOrderedEnumerable<TEntity> _orderedQuery;
    internal EFCoreOrderedQuery(IOrderedEnumerable<TEntity> query) : base(query.AsQueryable())
    {
        _orderedQuery = query;
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence in ascending order
    /// by using a specified comparer <see cref="Comparer{}.Default"/>.
    /// </summary>
    /// <returns>An <see cref="EFCoreOrderedQuery{}"/> whose elements are sorted according to a key.</returns>
    public EFCoreOrderedQuery<TEntity> ThenBy<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        _orderedQuery = _orderedQuery.ThenBy(keySelector, comparer ?? Comparer<TProperty>.Default);
        _query = _orderedQuery.AsQueryable();
        return this;
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence in descending order
    /// by using a specified comparer or <see cref="Comparer{}.Default"/>.
    /// </summary>
    /// <returns>An <see cref="EFCoreOrderedQuery{}"/> whose elements are sorted in descending order according to a key.</returns>
    public EFCoreOrderedQuery<TEntity> ThenByDescending<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        _orderedQuery = _orderedQuery.ThenByDescending(keySelector, comparer ?? Comparer<TProperty>.Default);
        _query = _orderedQuery.AsQueryable();
        return this;
    }
}
