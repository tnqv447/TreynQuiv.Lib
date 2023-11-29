using Microsoft.AspNetCore.Builder;

namespace TranQuiv.Lib.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Add X-Content-Type-Options: nosniff to HTTP responses
    /// </summary>
    public static IApplicationBuilder UseNoSniffHeader(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            context.Response.Headers.XContentTypeOptions = "nosniff";
            await next();
        });
    }

    /// <summary>
    /// Add X-Frame-Options: SAMEORIGIN to HTTP responses
    /// </summary>
    public static IApplicationBuilder UseXFrameOptions(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            context.Response.Headers.XFrameOptions = "SAMEORIGIN";
            await next();
        });
    }
}
