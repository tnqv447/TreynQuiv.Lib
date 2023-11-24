using System.Text.Encodings.Web;
using System.Text.Json;

namespace TranQuiv.Lib.Json.Extensions;

public static class JsonSerializerOptionsExtensions
{
    /// <summary>
    /// Fluent API for setting <see cref="JsonNamingPolicy"/> to <see cref="JsonNamingPolicy.CamelCase"/>.
    /// <para>If <paramref name="enable"/> is false, use default <see cref="null"/> instead.</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    public static JsonSerializerOptions SetCamelCase(this JsonSerializerOptions option, bool enable = true)
    {
        option.PropertyNamingPolicy = enable ? JsonNamingPolicy.CamelCase : null;
        return option;
    }

    /// <summary>
    /// Fluent API for setting <see cref="JsonNamingPolicy"/> to <see cref="JsonNamingPolicy.CamelCase"/>.
    /// <para>If <paramref name="enable"/> is false, use default instead.</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    public static JsonSerializerOptions SetWriteIndented(this JsonSerializerOptions option, bool enable = true)
    {
        option.WriteIndented = enable;
        return option;
    }

    public static JsonSerializerOptions SetCaseInsensitive(this JsonSerializerOptions option, bool enable = true)
    {
        option.PropertyNameCaseInsensitive = enable;
        return option;
    }

    public static JsonSerializerOptions SetUnsafeRelaxedEncoding(this JsonSerializerOptions option, bool enable = true)
    {
        option.Encoder = enable ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping : JavaScriptEncoder.Default;
        return option;
    }
}
