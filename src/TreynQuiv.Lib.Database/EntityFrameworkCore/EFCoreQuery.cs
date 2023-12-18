using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TreynQuiv.Lib.Components;

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
    public EFCoreQuery<TEntity> Include<TProperty>(string propertyName)
    {
        _query = _query.Include(propertyName);
        return this;
    }

    public EFCoreIncludedQuery<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> includeExpression)
    {
        return new(_query.Include(includeExpression));
    }

    public EFCoreOrderedQuery<TEntity> OrderBy<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        return new(_query.OrderBy(keySelector, comparer ?? Comparer<TProperty>.Default));
    }

    public EFCoreOrderedQuery<TEntity> OrderByDescending<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        return new(_query.OrderByDescending(keySelector, comparer ?? Comparer<TProperty>.Default));
    }
}

public record class EFCoreIncludedQuery<TEntity, TProperty> : EFCoreQuery<TEntity> where TEntity : class, IEntity
{
    private readonly IIncludableQueryable<TEntity, TProperty> _includeQuery;
    internal EFCoreIncludedQuery(IIncludableQueryable<TEntity, TProperty> query) : base(query)
    {
        _includeQuery = query;
    }

    public EFCoreIncludedQuery<TEntity, TPropertyMember> ThenInclude<TPropertyMember>(Expression<Func<TProperty, TPropertyMember>> includeExpression)
    {
        return new(_includeQuery.ThenInclude(includeExpression));
    }
}

public record class EFCoreOrderedQuery<TEntity> : EFCoreQuery<TEntity> where TEntity : class, IEntity
{
    private IOrderedEnumerable<TEntity> _orderedQuery;
    internal EFCoreOrderedQuery(IOrderedEnumerable<TEntity> query) : base(query.AsQueryable())
    {
        _orderedQuery = query;
    }

    public EFCoreOrderedQuery<TEntity> ThenBy<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        _orderedQuery = _orderedQuery.ThenBy(keySelector, comparer ?? Comparer<TProperty>.Default);
        _query = _orderedQuery.AsQueryable();
        return this;
    }
    public EFCoreOrderedQuery<TEntity> ThenByDescending<TProperty>(Func<TEntity, TProperty> keySelector, IComparer<TProperty>? comparer = null)
    {
        _orderedQuery = _orderedQuery.ThenByDescending(keySelector, comparer ?? Comparer<TProperty>.Default);
        _query = _orderedQuery.AsQueryable();
        return this;
    }
}
