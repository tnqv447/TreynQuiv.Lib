using System.Text.Json;

namespace TranQuiv.Lib.Extensions.Json;

public static class JsonSerializerOptionsFactory
{
    public static JsonSerializerOptions Instance
    {
        get
        {
            var option = new JsonSerializerOptions();
            option.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            option.PropertyNameCaseInsensitive = true;
            return option;
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
