using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Extensions;

public static class IRowExtensions
{
    /// <summary>
    /// Get or create a cell with the enforced <see cref="CellType"/>.
    /// </summary>
    public static ICell EnsureCell(this IRow row, int columnIndex, CellType cellType = CellType.Blank)
    {
        var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
        cell.SetCellType(cellType);
        return cell;
    }
}
