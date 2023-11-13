using System.ComponentModel;
using System.Reflection;

namespace TranQuiv.Lib.Extensions.Enum;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue) where T : System.Enum
    {
        var name = enumValue.ToString();
        return enumValue.GetType()
                        .GetMember(name)
                        .FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? name;
    }
}
