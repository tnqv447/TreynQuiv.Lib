using System.Text.Json;
using TranQuiv.Lib.Json.Extensions;

namespace TranQuiv.Lib.Json;

/// <summary>
/// A class to get many commonly used <see cref="JsonSerializerOptions"/>
/// </summary>
public static class JsonSerializerOptionsFactory
{
    /// <summary>
    /// Default value with options: <c>CaseInsensitive, UnsafeRelaxedJsonEscaping</c>
    /// </summary>
    /// <value></value>
    public static JsonSerializerOptions Instance
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

    public static JsonSerializerOptions IndentedCamelCase
    {
        get => Instance.SetCamelCase().SetWriteIndented();
    }

    public static JsonSerializerOptions NotIndentedCamelCase
    {
        get => Instance.SetCamelCase().SetWriteIndented(false);
    }

    public static JsonSerializerOptions Indented
    {
        get => Instance.SetWriteIndented();
    }

    public static JsonSerializerOptions NotIndented
    {
        get => Instance.SetWriteIndented(false);
    }
}
