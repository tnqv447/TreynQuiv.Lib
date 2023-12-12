using NPOI.SS.UserModel;

namespace TreynQuiv.Lib.NPOI;

/// <summary>
/// Base interface for configuring common properties of <see cref="IFont"/>.
/// </summary>
public interface IFontBuilder
{
    /// <summary>
    /// Compiles and returns a <see cref="IFont"/>.
    /// </summary>
    IFont Export();

    /// <summary>
    /// Clones from another <see cref="IFont"/>.
    /// </summary>
    IFontBuilder Clone(IFont font);

    /// <summary>
    /// Clones from another <see cref="IFontBuilder"/>.
    /// </summary>
    IFontBuilder Clone(IFontBuilder otherFontBuilder);

    /// <summary>
    /// Set font name
    /// </summary>
    /// <param name="fontName"></param>
    /// <returns></returns>
    IFontBuilder FontName(string fontName);

    /// <summary>
    /// Enable bold font.
    /// <para>If <paramref name="enable"/> is <see langword="false"/>, disable bold font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Bold(bool enable = true);

    /// <summary>
    /// Enable italic font.
    /// <para>If <paramref name="enable"/> is <see langword="false"/>, disable italic font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Italic(bool enable = true);

    /// <summary>
    /// Enable strikeout font.
    /// <para>If <paramref name="enable"/> is <see langword="false"/>, disable strikeout font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Strikeout(bool enable = true);

    /// <summary>
    /// Set font color
    /// </summary>
    IFontBuilder Color(IColor color);

    /// <summary>
    /// Set font color
    /// </summary>
    IFontBuilder Color(IndexedColors color);

    /// <summary>
    /// Set font color with <see langword="short"/> value.
    /// </summary>
    IFontBuilder Color(short color);

    /// <summary>
    /// Set font's height in points
    /// </summary>
    IFontBuilder HeightInPoints(short value);

    /// <summary>
    /// Set font's underline type.
    /// </summary>
    IFontBuilder Underline(FontUnderlineType underlineType);

    /// <summary>
    /// Set font's type offset
    /// </summary>
    IFontBuilder TypeOffset(FontSuperScript fontSuperScript);
}
