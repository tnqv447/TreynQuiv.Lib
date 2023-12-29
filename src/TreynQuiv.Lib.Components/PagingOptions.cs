using System.Diagnostics.CodeAnalysis;

namespace TreynQuiv.Lib.Components;

/// <summary>
/// Object for settings pagination options.
/// </summary>
public class PagingOptions
{
    private int _pageSize;

    /// <summary>
    /// Number of elements per page.
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// Start page index of <see cref="PageRange"/> using 0-based indices.
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// Last page index of <see cref="PageRange"/> using 0-based indices.
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// Get the range of <see cref="FromPage"/> to <see cref="ToPage"/>
    /// </summary>
    /// <value></value>
    public int PageRange
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
