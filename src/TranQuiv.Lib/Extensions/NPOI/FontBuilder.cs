using NPOI.SS.UserModel;

namespace TranQuiv.Lib.Extensions.NPOI;

public class FontBuilder
{
    private readonly IWorkbook _wb;
    private readonly IFont _font;
    public FontBuilder(IWorkbook wb)
    {
        _wb = wb;
        _font = _wb.CreateFont();
        _font.FontName = "Arial";
        _font.FontHeightInPoints = 12;
    }

    public IFont Create()
    {
        return _font;
    }

    public FontBuilder FontName(string fontName)
    {
        _font.FontName = fontName;
        return this;
    }

    public FontBuilder Bold()
    {
        _font.IsBold = true;
        return this;
    }

    public FontBuilder Italic()
    {
        _font.IsItalic = true;
        return this;
    }

    public FontBuilder Strikeout()
    {
        _font.IsStrikeout = true;
        return this;
    }

    public FontBuilder Color(FontColor color)
    {
        _font.Color = (short)color;
        return this;
    }

    public FontBuilder HeightInPoints(short value)
    {
        _font.FontHeightInPoints = value;
        return this;
    }

    public FontBuilder Underline(FontUnderlineType underlineType)
    {
        _font.Underline = underlineType;
        return this;
    }

    public FontBuilder Clone(IFont otherFont)
    {
        return this;
    }
}

