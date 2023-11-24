using NPOI.SS.UserModel;

namespace TranQuiv.Lib.NPOI.Extensions;

public static class IRowExtensions
{
    /// <summary>
    /// Get or create a cell with enforced <paramref name="cellType"/>.
    /// </summary>
    public static ICell EnsureCell(this IRow row, int columnIndex, CellType cellType = CellType.Blank)
    {
        var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
        cell.SetCellType(cellType);
        return cell;
    }
}
