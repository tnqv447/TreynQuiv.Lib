using System.ComponentModel;
using System.Reflection;

namespace TreynQuiv.Lib.Common.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get value of <see cref="DescriptionAttribute"/> of this <paramref name="enumValue"/>.
    /// </summary>
    /// <remarks>Return property name if there is no <see cref="DescriptionAttribute"/> added to this value</remarks>
    /// <returns>The description or the string value of <paramref name="enumValue"/> if <see cref="DescriptionAttribute"/> is not found.</returns>
    public static string GetDescription(this Enum enumValue)
    {
        var name = enumValue.ToString();
        return enumValue.GetType().GetMember(name)
            .FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? name;
    }

    /// <summary>
    /// Get the <typeparamref name="TAttribute"/> of this <paramref name="enumValue"/>.
    /// </summary>
    /// <returns>A custom attribute that matches <typeparamref name="TAttribute"/>, or <see langword="null"/> if no such attribute is found.</returns>
    public static TAttribute? GetCustomAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
    {
        var name = enumValue.ToString();
        return enumValue.GetType().GetMember(name)
            .FirstOrDefault()?.GetCustomAttribute<TAttribute>();
    }
}
