using NPOI.SS.UserModel;
using TreynQuiv.Lib.NPOI.Components;

namespace TreynQuiv.Lib.NPOI.Extensions;

public static class IWorkbookExtensions
{
    /// <summary>
    /// Initialize a built-in <see cref="ICellStyleBuilder"/>.
    /// </summary>
    public static ICellStyleBuilder CellStyleBuilder(this IWorkbook wb)
    {
        return new CellStyleBuilder(wb);
    }

    /// <summary>
    /// Initialize a built-in <see cref="IFontBuilder"/>.
    /// </summary>
    public static IFontBuilder FontBuilder(this IWorkbook wb)
    {
        return new FontBuilder(wb);
    }
}
