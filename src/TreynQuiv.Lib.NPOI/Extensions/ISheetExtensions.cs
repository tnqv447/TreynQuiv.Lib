using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Extensions;

public static class ISheetExtensions
{
    /// <summary>
    /// Get or create a <see cref="IRow"/>.
    /// </summary>
    public static IRow EnsureRow(this ISheet sheet, int rowIndex)
    {
        return sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
    }
}
