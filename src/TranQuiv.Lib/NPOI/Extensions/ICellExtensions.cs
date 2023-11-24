using NPOI.SS.UserModel;

namespace TranQuiv.Lib.NPOI.Extensions;

public static class ICellExtensions
{
    public static void SetValue(this ICell cell, dynamic value, ICellStyle cellStyle, bool cloneStyle = false)
    {
        if (cloneStyle)
        {
            cell.CellStyle.CloneStyleFrom(cellStyle);
        }
        else
        {
            cell.CellStyle = cellStyle;
        }

        switch (cell.CellType)
        {
            case CellType.Formula:
                cell.SetCellFormula((string)value);
                break;

            default:
                cell.SetCellValue(value);
                break;
        }
    }
}
