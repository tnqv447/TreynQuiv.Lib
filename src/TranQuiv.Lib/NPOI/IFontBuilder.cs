using NPOI.SS.UserModel;

namespace TranQuiv.Lib.NPOI;

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
    /// <para>If <paramref name="enable"/> is false, disable bold font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Bold(bool enable = true);

    /// <summary>
    /// Enable italic font.
    /// <para>If <paramref name="enable"/> is false, disable italic font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Italic(bool enable = true);

    /// <summary>
    /// Enable strikeout font.
    /// <para>If <paramref name="enable"/> is false, disable strikeout font in this <see cref="IFontBuilder"/>.</para>
    /// </summary>
    IFontBuilder Strikeout(bool enable = true);
    IFontBuilder Color(IColor color);
    IFontBuilder Color(IndexedColors color);
    IFontBuilder Color(short color);
    IFontBuilder HeightInPoints(short value);
    IFontBuilder Underline(FontUnderlineType underlineType);
    IFontBuilder TypeOffset(FontSuperScript fontSuperScript);
}
