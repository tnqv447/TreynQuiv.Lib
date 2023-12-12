using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI.Components;

/// <summary>
/// Built-in implementation of <see cref="IFontBuilder"/>.
/// </summary>
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

    public IFont Export()
    {
        var exportFont = _wb.CreateFont();
        exportFont.CloneStyleFrom(_font);
        return exportFont;
    }

    public IFontBuilder Clone(IFont font)
    {
        _font.CloneStyleFrom(font);
        return this;
    }

    public IFontBuilder Clone(IFontBuilder otherFontBuilder)
    {
        _font.CloneStyleFrom(otherFontBuilder.Export());
        return this;
    }

    public IFontBuilder FontName(string fontName)
    {
        _font.FontName = fontName;
        return this;
    }

    public IFontBuilder Bold(bool enable = true)
    {
        _font.IsBold = enable;
        return this;
    }

    public IFontBuilder Italic(bool enable = true)
    {
        _font.IsItalic = enable;
        return this;
    }

    public IFontBuilder Strikeout(bool enable = true)
    {
        _font.IsStrikeout = enable;
        return this;
    }

    public IFontBuilder Color(IColor color)
    {
        _font.Color = color.Indexed;
        return this;
    }

    public IFontBuilder Color(IndexedColors color)
    {
        _font.Color = color.Index;
        return this;
    }

    public IFontBuilder Color(short color)
    {
        _font.Color = color;
        return this;
    }

    public IFontBuilder HeightInPoints(short value)
    {
        _font.FontHeightInPoints = value;
        return this;
    }

    public IFontBuilder Underline(FontUnderlineType underlineType)
    {
        _font.Underline = underlineType;
        return this;
    }

    public IFontBuilder TypeOffset(FontSuperScript fontSuperScript)
    {
        _font.TypeOffset = fontSuperScript;
        return this;
    }
}
