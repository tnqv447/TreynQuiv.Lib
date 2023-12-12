using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Extensions;

public static class ICellExtensions
{
    /// <summary>
    /// Fluently set cell value and it's <see cref="CellType"/> based on type of <paramref name="value"/>.
    /// </summary>
    /// <returns>This <see cref="ICell"/>.</returns>
    public static ICell SetValue(this ICell cell, dynamic value)
    {
        cell.SetCellValue(value);
        return cell;
    }

    /// <summary>
    /// Fluently set cell formula.
    /// </summary>
    /// <returns>This <see cref="ICell"/>.</returns>
    public static ICell SetFormula(this ICell cell, string formula)
    {
        cell.SetCellFormula(formula);
        return cell;
    }

    /// <summary>
    /// Fluently set cell's <see cref="ICellStyle"/>.
    /// <para>If <paramref name="cloneStyle"/> is <see langword="true"/>, set using new instance of <paramref name="cellStyle"/> instead of the same reference.</para>
    /// </summary>
    /// <returns>This <see cref="ICell"/>.</returns>
    public static ICell SetStyle(this ICell cell, ICellStyle cellStyle, bool cloneStyle = false)
    {
        if (cloneStyle)
        {
            cell.CellStyle.CloneStyleFrom(cellStyle);
        }
        else
        {
            cell.CellStyle = cellStyle;
        }

        return cell;
    }
}
