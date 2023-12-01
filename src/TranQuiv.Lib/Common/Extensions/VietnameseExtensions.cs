using System.Globalization;
using Humanizer;

namespace TranQuiv.Lib.Common.Extensions;

public static class VietnameseExtensions
{
    private static CultureInfo VietnameseCulture
    {
        get => new("vi-VN");
    }

    /// <summary>
    /// Convert value to words Vietnamese representation.
    /// </summary>
    /// <remarks>Eg: 3501 => "ba nghìn năm trăm linh một"</remarks>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToVietnameseWords(this long value)
    {
        return value.ToWords(VietnameseCulture);
    }

    /// <summary>
    /// Convert value to words Vietnamese representation.
    /// </summary>
    /// <remarks>Eg: 3501 => "ba nghìn năm trăm linh một"</remarks>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToVietnameseWords(this int value)
    {
        return value.ToWords(VietnameseCulture);
    }

    /// <summary>
    /// Get value's number string representation in Vietnamese
    /// </summary>
    /// <remarks><em>Eg: 100000.33 => 100,000.33</em></remarks>
    /// <param name="value"></param>
    /// <param name="useSymbol"></param>
    /// <returns></returns>
    public static string ToVietnameseNumberString(this double value, int rounding = 0, bool useSymbol = false)
    {
        var culture = VietnameseCulture;
        culture.NumberFormat = new NumberFormatInfo
        {
            CurrencySymbol = useSymbol ? "₫" : string.Empty,
            CurrencyPositivePattern = 3,
            CurrencyNegativePattern = 3,
            CurrencyDecimalSeparator = ".",
            CurrencyGroupSeparator = ",",
            CurrencyDecimalDigits = 0
        };
        return Math.Round(value, rounding).ToString("C", culture);
    }

    /// <summary>
    /// Get value's number string representation in Vietnamese
    /// </summary>
    /// <remarks><em>Eg: 10000000 => 10,000,000</em></remarks>
    /// <param name="value"></param>
    /// <param name="useSymbol"></param>
    /// <returns></returns>
    public static string ToVietnameseNumberString(this long value, bool useSymbol = false)
    {
        var culture = VietnameseCulture;
        culture.NumberFormat = new NumberFormatInfo
        {
            CurrencySymbol = useSymbol ? "₫" : string.Empty,
            CurrencyPositivePattern = 3,
            CurrencyNegativePattern = 3,
            CurrencyDecimalSeparator = ".",
            CurrencyGroupSeparator = ",",
            CurrencyDecimalDigits = 0
        };
        return value.ToString("C", culture);
    }

    /// <summary>
    /// Get value's number string representation in Vietnamese
    /// </summary>
    /// <remarks><em>Eg: 10000000 => 10,000,000</em></remarks>
    /// <param name="value"></param>
    /// <param name="useSymbol"></param>
    /// <returns></returns>
    public static string ToVietnameseNumberString(this int value, bool useSymbol = false)
    {
        var culture = VietnameseCulture;
        culture.NumberFormat = new NumberFormatInfo
        {
            CurrencySymbol = useSymbol ? "₫" : string.Empty,
            CurrencyPositivePattern = 3,
            CurrencyNegativePattern = 3,
            CurrencyDecimalSeparator = ".",
            CurrencyGroupSeparator = ",",
            CurrencyDecimalDigits = 0
        };
        return value.ToString("C", culture);
    }
}
