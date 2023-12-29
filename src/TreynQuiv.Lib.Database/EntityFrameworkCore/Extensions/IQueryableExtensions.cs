using TreynQuiv.Lib.Components;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore.Extensions;

internal static class IQueryableExtensions
{
    public static IQueryable<T> Paging<T>(this IQueryable<T> query, PagingOptions pagingOptions)
    {
        return query.Skip(pagingOptions.PageSize * pagingOptions.FromPage)
            .Take(pagingOptions.PageSize * pagingOptions.PageRange);
    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> query, PagingOptions pagingOptions, out long totalRecords, out long totalPages)
    {
        totalRecords = query.LongCount();
        totalPages = Math.DivRem(totalRecords, pagingOptions.PageSize, out var rem);
        totalPages += rem > 0 ? 1 : 0;
        return query.Paging(pagingOptions);
    }
}
