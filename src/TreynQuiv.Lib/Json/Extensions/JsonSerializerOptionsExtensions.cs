using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace TreynQuiv.Lib.Json.Extensions;

public static class JsonSerializerOptionsExtensions
{
    /// <summary>
    /// Fluent API for setting <see cref="JsonNamingPolicy"/> to <see cref="JsonNamingPolicy.CamelCase"/>.
    /// <para>If <paramref name="enable"/> is <see langword="false"/>, use default <see cref="null"/> instead.</para>
    /// </summary>
    public static JsonSerializerOptions SetCamelCase(this JsonSerializerOptions option, bool enable = true)
    {
        option.PropertyNamingPolicy = enable ? JsonNamingPolicy.CamelCase : null;
        return option;
    }

    /// <summary>
    /// Fluent API for enable write indented.
    /// </summary>
    public static JsonSerializerOptions SetWriteIndented(this JsonSerializerOptions option, bool enable = true)
    {
        option.WriteIndented = enable;
        return option;
    }

    /// <summary>
    /// Fluent API for enable case insensitive.
    /// </summary>
    public static JsonSerializerOptions SetCaseInsensitive(this JsonSerializerOptions option, bool enable = true)
    {
        option.PropertyNameCaseInsensitive = enable;
        return option;
    }

    /// <summary>
    /// Fluent API make this instance readonly. Can't revert
    /// </summary>
    public static JsonSerializerOptions MakeReadonlyInline(this JsonSerializerOptions option)
    {
        option.TypeInfoResolver = JsonSerializerOptions.Default.TypeInfoResolver;
        option.MakeReadOnly();
        return option;
    }
}
