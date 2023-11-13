using System.Text.Json;

namespace TranQuiv.Lib.Extensions.Json;

public static class JsonExtensions
{
    public static string ToJsonString(this object obj, bool writeIndented = false)
    {
        dynamic @this = Convert.ChangeType(obj, obj.GetType());
        return JsonSerializer.Serialize(@this, options: writeIndented ? JsonSerializerOptionsFactory.Indented : JsonSerializerOptionsFactory.NotIndented);
    }

    public static string ToJsonString(this object obj, JsonSerializerOptions? options)
    {
        dynamic @this = Convert.ChangeType(obj, obj.GetType());
        return JsonSerializer.Serialize(@this, options);
    }
}
