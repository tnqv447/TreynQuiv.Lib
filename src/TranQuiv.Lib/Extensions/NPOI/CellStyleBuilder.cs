using NPOI.SS.UserModel;

namespace TranQuiv.Lib.Extensions.NPOI;

public class CellStyleBuilder
{
    private readonly IWorkbook _wb;
    private readonly ICellStyle _style;
    public CellStyleBuilder(IWorkbook wb)
    {
        _wb = wb;
        _style = _wb.CreateCellStyle();
    }

    public ICellStyle Create()
    {
        return _style;
    }

    public CellStyleBuilder Font(IFont font)
    {
        _style.SetFont(font);
        return this;
    }

    public CellStyleBuilder WrapText()
    {
        _style.WrapText = true;
        return this;
    }

    public CellStyleBuilder ForegroundColor(IColor color)
    {
        _style.FillForegroundColor = color.Indexed;
        return this;
    }

    public CellStyleBuilder BackgroundColor(IColor color)
    {
        _style.FillBackgroundColor = color.Indexed;
        return this;
    }

    public CellStyleBuilder Border(BorderStyle fullBorderStyle)
    {
        _style.BorderBottom = fullBorderStyle;
        _style.BorderTop = fullBorderStyle;
        _style.BorderLeft = fullBorderStyle;
        _style.BorderRight = fullBorderStyle;
        return this;
    }

    public CellStyleBuilder Border(BorderStyle topBottomBorderStyle, BorderStyle leftRightBorderStyle)
    {
        _style.BorderBottom = topBottomBorderStyle;
        _style.BorderTop = topBottomBorderStyle;
        _style.BorderLeft = leftRightBorderStyle;
        _style.BorderRight = leftRightBorderStyle;
        return this;
    }

    public CellStyleBuilder Border(BorderStyle topBorderStyle, BorderStyle bottomBorderStyle, BorderStyle leftBorderStyle, BorderStyle rightBorderStyle)
    {
        _style.BorderBottom = bottomBorderStyle;
        _style.BorderTop = topBorderStyle;
        _style.BorderLeft = leftBorderStyle;
        _style.BorderRight = rightBorderStyle;
        return this;
    }

    public CellStyleBuilder BorderColor(IColor color)
    {
        _style.BottomBorderColor = color.Indexed;
        _style.TopBorderColor = color.Indexed;
        _style.LeftBorderColor = color.Indexed;
        _style.RightBorderColor = color.Indexed;
        return this;
    }

    public CellStyleBuilder BorderColor(IColor topBottomColor, IColor leftRightColor)
    {
        _style.BottomBorderColor = topBottomColor.Indexed;
        _style.TopBorderColor = topBottomColor.Indexed;
        _style.LeftBorderColor = leftRightColor.Indexed;
        _style.RightBorderColor = leftRightColor.Indexed;
        return this;
    }

    public CellStyleBuilder BorderColor(IColor topColor, IColor bottomColor, IColor leftColor, IColor rightColor)
    {
        _style.BottomBorderColor = topColor.Indexed;
        _style.TopBorderColor = bottomColor.Indexed;
        _style.LeftBorderColor = leftColor.Indexed;
        _style.RightBorderColor = rightColor.Indexed;
        return this;
    }

    public CellStyleBuilder DataFormat(string formatString)
    {
        _style.DataFormat = _wb.CreateDataFormat().GetFormat(formatString);
        return this;
    }

    public CellStyleBuilder Clone(ICellStyle otherStyle)
    {
        _style.CloneStyleFrom(otherStyle);
        return this;
    }

    public CellStyleBuilder Alignment(HorizontalAlignment horizontalAlignment = HorizontalAlignment.General,
                                      VerticalAlignment verticalAlignment = VerticalAlignment.None)
    {
        _style.Alignment = horizontalAlignment;
        _style.VerticalAlignment = verticalAlignment;
        return this;
    }
}
