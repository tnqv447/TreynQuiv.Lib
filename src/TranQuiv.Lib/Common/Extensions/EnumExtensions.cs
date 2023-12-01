using System.ComponentModel;
using System.Reflection;

namespace TranQuiv.Lib.Common.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get value of <see cref="DescriptionAttribute"/> of this enum value
    /// </summary>
    /// <remarks>Return property name if there is no <see cref="DescriptionAttribute"/> added to this value</remarks>
    /// <param name="enumValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>The description or enum string value if <see cref="DescriptionAttribute"/> not found.</returns>
    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
        var name = enumValue.ToString();
        return enumValue.GetType().GetMember(name)
            .FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? name;
    }
}
