using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Components;

internal class CellStyleBuilder : ICellStyleBuilder
{
    private readonly IWorkbook _wb;
    private readonly ICellStyle _style;

    public CellStyleBuilder(IWorkbook wb)
    {
        _wb = wb;
        _style = _wb.CreateCellStyle();
    }

    public virtual ICellStyle Export()
    {
        var exportStyle = _wb.CreateCellStyle();
        exportStyle.CloneStyleFrom(_style);
        return exportStyle;
    }

    public virtual ICellStyleBuilder Clone(ICellStyle otherStyle)
    {
        _style.CloneStyleFrom(otherStyle);
        return this;
    }

    public virtual ICellStyleBuilder Clone(ICellStyleBuilder otherStyleBuilder)
    {
        _style.CloneStyleFrom(otherStyleBuilder.Export());
        return this;
    }

    public virtual ICellStyleBuilder Font(IFont font)
    {
        _style.SetFont(font);
        return this;
    }

    public virtual ICellStyleBuilder WrapText(bool enable = true)
    {
        _style.WrapText = enable;
        return this;
    }

    public virtual ICellStyleBuilder Color(IColor color)
    {
        _style.FillForegroundColor = color.Indexed;
        return this;
    }

    public virtual ICellStyleBuilder BackgroundColor(IColor color)
    {
        _style.FillBackgroundColor = color.Indexed;
        return this;
    }

    public virtual ICellStyleBuilder Border(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
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

    public virtual ICellStyleBuilder BorderTop(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderTop = borderStyle;
        _style.TopBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public virtual ICellStyleBuilder BorderBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderBottom = borderStyle;
        _style.BottomBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public virtual ICellStyleBuilder BorderLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderLeft = borderStyle;
        _style.LeftBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public virtual ICellStyleBuilder BorderRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderRight = borderStyle;
        _style.RightBorderColor = color is not null ? color.Indexed : IndexedColors.Black.Index;
        return this;
    }

    public virtual ICellStyleBuilder BorderTopRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderTopLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderLeft(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderBottomRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderBottom(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderBottomLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderBottom(borderStyle, color);
        BorderLeft(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderTopBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderTop(borderStyle, color);
        BorderBottom(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderLeftRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        BorderLeft(borderStyle, color);
        BorderRight(borderStyle, color);
        return this;
    }

    public virtual ICellStyleBuilder BorderDiagonal(BorderDiagonal diagonal, BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null)
    {
        _style.BorderDiagonal = diagonal;
        _style.BorderDiagonalLineStyle = borderStyle;
        _style.BorderDiagonalColor = color?.Indexed ?? IndexedColors.Black.Index;
        return this;
    }

    public virtual ICellStyleBuilder DataFormat(string formatString)
    {
        _style.DataFormat = _wb.CreateDataFormat().GetFormat(formatString);
        return this;
    }

    public virtual ICellStyleBuilder HorizontalAlignment(HorizontalAlignment alignment)
    {
        _style.Alignment = alignment;
        return this;
    }

    public virtual ICellStyleBuilder VerticalAlignment(VerticalAlignment alignment)
    {
        _style.VerticalAlignment = alignment;
        return this;
    }
}
