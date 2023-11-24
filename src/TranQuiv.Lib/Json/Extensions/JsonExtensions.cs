using System.Text.Json;

namespace TranQuiv.Lib.Json.Extensions;

public static class JsonExtensions
{
    /// <summary>
    /// Get the JSON representation of this object
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="writeIndented"></param>
    /// <returns></returns>
    public static string ToJsonString(this object obj, bool writeIndented = false)
    {
        dynamic @this = Convert.ChangeType(obj, obj.GetType());
        return JsonSerializer.Serialize(@this, options: writeIndented ? JsonSerializerOptionsFactory.Indented : JsonSerializerOptionsFactory.NotIndented);
    }

    /// <summary>
    /// Get the JSON representation of this object with options
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string ToJsonString(this object obj, JsonSerializerOptions? options)
    {
        dynamic @this = Convert.ChangeType(obj, obj.GetType());
        return JsonSerializer.Serialize(@this, options);
    }
}
