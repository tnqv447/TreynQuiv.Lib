using NPOI.SS.UserModel;

namespace TranQuiv.Lib.NPOI.Components;

internal class FontBuilder : IFontBuilder
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

    public virtual IFont Export()
    {
        return _font;
    }

    public virtual IFontBuilder Clone(IFont font)
    {
        _font.CloneStyleFrom(font);
        return this;
    }

    public virtual IFontBuilder Clone(IFontBuilder otherFontBuilder)
    {
        _font.CloneStyleFrom(otherFontBuilder.Export());
        return this;
    }

    public virtual IFontBuilder FontName(string fontName)
    {
        _font.FontName = fontName;
        return this;
    }

    public virtual IFontBuilder Bold(bool enable = true)
    {
        _font.IsBold = enable;
        return this;
    }

    public virtual IFontBuilder Italic(bool enable = true)
    {
        _font.IsItalic = enable;
        return this;
    }

    public virtual IFontBuilder Strikeout(bool enable = true)
    {
        _font.IsStrikeout = enable;
        return this;
    }

    public virtual IFontBuilder Color(IColor color)
    {
        _font.Color = color.Indexed;
        return this;
    }

    public virtual IFontBuilder Color(IndexedColors color)
    {
        _font.Color = color.Index;
        return this;
    }

    public virtual IFontBuilder Color(short color)
    {
        _font.Color = color;
        return this;
    }

    public virtual IFontBuilder HeightInPoints(short value)
    {
        _font.FontHeightInPoints = value;
        return this;
    }

    public virtual IFontBuilder Underline(FontUnderlineType underlineType)
    {
        _font.Underline = underlineType;
        return this;
    }

    public virtual IFontBuilder TypeOffset(FontSuperScript fontSuperScript)
    {
        _font.TypeOffset = fontSuperScript;
        return this;
    }
}

