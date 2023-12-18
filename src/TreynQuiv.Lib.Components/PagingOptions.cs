using System.Diagnostics.CodeAnalysis;

namespace TreynQuiv.Lib.Components;

public class PagingOptions
{
    private int _pageSize;
    public required int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(PageSize)} must not be negative");
            }

            _pageSize = value;
        }
    }

    private int _fromPage;
    public required int FromPage
    {
        get => _fromPage;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(FromPage)} must not be negative");
            }

            _fromPage = value;
        }
    }

    private int _toPage;
    public required int ToPage
    {
        get => _toPage;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException($"{nameof(ToPage)} must be bigger than {nameof(FromPage)}");
            }

            _toPage = value;
        }
    }
    public int PageRangeSize
    {
        get => ToPage - FromPage;
    }

    [SetsRequiredMembers]
    public PagingOptions(int pageSize, int pageIndex)
    {
        PageSize = pageSize;
        FromPage = pageIndex;
        ToPage = pageIndex + 1;
    }

    [SetsRequiredMembers]
    public PagingOptions(int pageSize, int fromPage, int toPage)
    {
        PageSize = pageSize;
        FromPage = fromPage;
        ToPage = toPage;
    }
}
