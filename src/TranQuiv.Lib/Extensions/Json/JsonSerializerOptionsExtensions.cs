using System.Text.Json;

namespace TranQuiv.Lib.Extensions.Json;

public static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions SetCamelCase(this JsonSerializerOptions option, bool useCamelCase = true)
    {
        option.PropertyNamingPolicy = useCamelCase ? JsonNamingPolicy.CamelCase : null;
        return option;
    }

    public static JsonSerializerOptions SetWriteIndented(this JsonSerializerOptions option, bool writeIndented = true)
    {
        option.WriteIndented = writeIndented;
        return option;
    }

    public static JsonSerializerOptions SetCaseInsensitive(this JsonSerializerOptions option, bool caseInsensitive = true)
    {
        option.PropertyNameCaseInsensitive = caseInsensitive;
        return option;
    }
}
