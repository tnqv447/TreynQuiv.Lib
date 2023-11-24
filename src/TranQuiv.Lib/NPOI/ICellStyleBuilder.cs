using NPOI.SS.UserModel;

namespace TranQuiv.Lib.NPOI;

public interface ICellStyleBuilder
{
    /// <summary>
    /// Builder compiles and return a <see cref="ICellStyle"/>.
    /// </summary>
    ICellStyle Export();

    /// <summary>
    /// Builder clone from another <see cref="ICellStyle"/>.
    /// </summary>
    ICellStyleBuilder Clone(ICellStyle otherStyle);

    /// <summary>
    /// Builder clone from another <see cref="ICellStyleBuilder"/>.
    /// </summary>
    ICellStyleBuilder Clone(ICellStyleBuilder otherStyleBuilder);

    /// <summary>
    /// Enables wrapping text. If <paramref name="enable"/> is false, disable instead.
    /// </summary>
    ICellStyleBuilder WrapText(bool enable = true);

    /// <summary>
    /// Set cell font.
    /// </summary>
    ICellStyleBuilder Font(IFont font);

    /// <summary>
    /// Set cell foreground color.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder Color(IColor color);

    /// <summary>
    /// Set cell background color.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BackgroundColor(IColor color);

    /// <summary>
    /// Set cell full border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder Border(BorderStyle fullBorderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell top border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderTop(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell bottom border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell left border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell right border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell top-left border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderTopLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell top-right border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderTopRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell bottom-left border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderBottomLeft(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell bottom-right border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderBottomRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell top-bottom border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderTopBottom(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell left-right border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderLeftRight(BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell diagonal border style.
    /// </summary>
    /// <remarks>To use <see cref="IndexedColors"/>, cast it to <see cref="IColor"/>.</remarks>
    ICellStyleBuilder BorderDiagonal(BorderDiagonal diagonal, BorderStyle borderStyle = BorderStyle.Thin, IColor? color = null);

    /// <summary>
    /// Set cell data format string.
    /// </summary>
    /// <remarks>
    /// <para>Useful for display <see cref="DateTime"/> as string, delimited number,...</para>
    /// <para>Look into <see cref="IDataFormat"/> for <paramref name="formatString"/> syntax.</para>
    /// </remarks>
    /// <param name="formatString">Look into <see cref="IDataFormat"/> for syntax.</param>
    ICellStyleBuilder DataFormat(string formatString);

    /// <summary>
    /// Set cell horizontal alignment.
    /// </summary>
    ICellStyleBuilder HorizontalAlignment(HorizontalAlignment alignment);

    /// <summary>
    /// Set cell vertical alignment.
    /// </summary>
    ICellStyleBuilder VerticalAlignment(VerticalAlignment alignment);
}
