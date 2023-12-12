using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace TreynQuiv.Lib.NPOI.Extensions;

public static class CellRangeAddressExtensions
{
    /// <summary>
    /// Fluently styling all the <see cref="ICell"/> in the <see cref="CellRangeAddress"/>.
    /// </summary>
    public static CellRangeAddress SetCellStyle(this CellRangeAddress region, ISheet sheet, ICellStyle cellStyle)
    {
        for (int rowIdx = region.FirstRow; rowIdx <= region.LastRow; ++rowIdx)
        {
            var row = sheet.EnsureRow(rowIdx);
            for (int colIdx = region.FirstColumn; colIdx <= region.LastColumn; ++colIdx)
            {
                var cell = row.EnsureCell(colIdx);
                cell.CellStyle = cellStyle;
            }
        }

        return region;
    }

    /// <summary>
    /// Fluently set value to the first <see cref="ICell"/> in the <see cref="CellRangeAddress"/>.
    /// </summary>
    public static CellRangeAddress SetValue(this CellRangeAddress region, ISheet sheet, dynamic value)
    {
        var cell = sheet.EnsureRow(region.FirstRow).EnsureCell(region.FirstColumn);
        cell.SetCellValue(value);
        return region;
    }

    /// <summary>
    /// Fluently set formula to the first <see cref="ICell"/> in the <see cref="CellRangeAddress"/>.
    /// </summary>
    public static CellRangeAddress SetFormula(this CellRangeAddress region, ISheet sheet, string formula)
    {
        var cell = sheet.EnsureRow(region.FirstRow).EnsureCell(region.FirstColumn, CellType.Formula);
        cell.SetCellFormula(formula);
        return region;
    }
}
