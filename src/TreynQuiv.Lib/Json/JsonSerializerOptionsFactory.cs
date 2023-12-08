using TreynQuiv.Lib.Json.Extensions;
using System.Text.Json;

namespace TreynQuiv.Lib.Json;

/// <summary>
/// A class to get many commonly used <see cref="JsonSerializerOptions"/>
/// </summary>
public static class JsonSerializerOptionsFactory
{
    /// <summary>
    /// Default value with enabled options: <see langword="CaseInsensitive, UnsafeRelaxedJsonEscaping"/>
    /// </summary>
    /// <value></value>
    public static JsonSerializerOptions Default { get; } = Instance.MakeReadonlyInline();
    /// <summary>
    /// Default value with enabled options: <see langword="CaseInsensitive, UnsafeRelaxedJsonEscaping"/>
    /// </summary>
    /// <value></value>
    private static JsonSerializerOptions Instance
    {
        get
        {
            return new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true
            };
        }
    }

    /// <summary>
    /// An instance that enabled <see langword="CamelCase"/> and <see langword="WriteIndented"/>.
    /// </summary>
    /// <value>A readonly instance</value>
    public static JsonSerializerOptions IndentedCamelCase
    {
        get => Instance.SetCamelCase().SetWriteIndented().MakeReadonlyInline();
    }

    /// <summary>
    /// An instance that enabled <see langword="CamelCase"/> and disabled <see langword="WriteIndented"/>.
    /// </summary>
    /// <value>A readonly instance</value>
    public static JsonSerializerOptions NotIndentedCamelCase
    {
        get => Instance.SetCamelCase().SetWriteIndented(false).MakeReadonlyInline();
    }

    /// <summary>
    /// An instance that enabled <see langword="WriteIndented"/>.
    /// </summary>
    /// <value>A readonly instance</value>
    public static JsonSerializerOptions Indented
    {
        get => Instance.SetWriteIndented().MakeReadonlyInline();
    }

    /// <summary>
    /// An instance that disabled <see langword="WriteIndented"/>.
    /// </summary>
    /// <value>A readonly instance</value>
    public static JsonSerializerOptions NotIndented
    {
        get => Instance.SetWriteIndented(false).MakeReadonlyInline();
    }
}
