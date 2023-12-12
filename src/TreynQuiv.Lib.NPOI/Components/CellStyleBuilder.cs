using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Components;

/// <summary>
/// Built-in implementation of <see cref="ICellStyleBuilder"/>.
/// </summary>
internal class CellStyleBuilder : ICellStyleBuilder
{
    private readonly IWorkbook _wb;
    private readonly ICellStyle _style;

    public CellStyleBuilder(IWorkbook wb)
    {
        _wb = wb;
        _style = _wb.CreateCellStyle();
    }

    public ICellStyle Export()
    {
        var exportStyle = _wb.CreateCellStyle();
        exportStyle.CloneStyleFrom(_style);
        return exportStyle;
    }

    public ICellStyleBuilder Clone(ICellStyle otherStyle)
    {
        _style.CloneStyleFrom(otherStyle);
        return this;
    }

    public ICellStyleBuilder Clone(ICellStyleBuilder otherStyleBuilder)
    {
        _style.CloneStyleFrom(otherStyleBuilder.Export());
        return this;
    }

    public ICellStyleBuilder Font(IFont font)
    {
        _style.SetFont(font);
        return this;
    }

    public ICellStyleBuilder WrapText(bool enable = true)
    {
        _style.WrapText = enable;
        return this;
    }

    public ICellStyleBuilder Color(IColor color)
    {
        _style.FillForegroundColor = color.Indexed;
        return this;
    }

    public ICellStyleBuilder BackgroundColor(IColor color)
    {
        _style.FillBackgroundColor = color.Indexed;
        return this;
    }

    public ICellStyleBuilder Border(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderBottom = borderStyle;
        _style.BorderTop = borderStyle;
        _style.BorderLeft = borderStyle;
        _style.BorderRight = borderStyle;

        var indexedColor = color ?? (IColor)IndexedColors.Black;
        _style.BottomBorderColor = indexedColor.Indexed;
        _style.TopBorderColor = indexedColor.Indexed;
        _style.LeftBorderColor = indexedColor.Indexed;
        _style.RightBorderColor = indexedColor.Indexed;
        return this;
    }

    public ICellStyleBuilder BorderTop(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderTop = borderStyle;
        _style.TopBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public ICellStyleBuilder BorderBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderBottom = borderStyle;
        _style.BottomBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public ICellStyleBuilder BorderLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderLeft = borderStyle;
        _style.LeftBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public ICellStyleBuilder BorderRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderRight = borderStyle;
        _style.RightBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public ICellStyleBuilder BorderTopRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderTopLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderLeft(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderBottomRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderBottom(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderBottomLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderBottom(borderStyle, color);
        BorderLeft(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderTopBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderBottom(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderLeftRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderLeft(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public ICellStyleBuilder BorderDiagonal(BorderDiagonal diagonal, BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderDiagonal = diagonal;
        _style.BorderDiagonalLineStyle = borderStyle;
        _style.BorderDiagonalColor = color?.Indexed ?? IndexedColors.Black.Index;
        return this;
    }

    public ICellStyleBuilder DataFormat(string formatString)
    {
        _style.DataFormat = _wb.CreateDataFormat().GetFormat(formatString);
        return this;
    }

    public ICellStyleBuilder HorizontalAlignment(HorizontalAlignment alignment)
    {
        _style.Alignment = alignment;
        return this;
    }

    public ICellStyleBuilder VerticalAlignment(VerticalAlignment alignment)
    {
        _style.VerticalAlignment = alignment;
        return this;
    }
}
