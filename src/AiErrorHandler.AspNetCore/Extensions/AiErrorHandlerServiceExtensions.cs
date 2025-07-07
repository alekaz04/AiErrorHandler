using AiErrorHandler.Core;
using AiErrorHandler.Models;
using Microsoft.AspNetCore.Builder;

namespace AiErrorHandler.AspNetCore;

public static class AiErrorHandlerServiceExtensions
{
    public static IApplicationBuilder UseAiErrorHandlerMiddleware(this IApplicationBuilder app, ConfigureAiHandler options)
    {
        AiErrorStaticAnalyse.InitializeGpt(options);
        app.UseMiddleware<AiErrorHandlerMiddleware>();

        return app;
    }
}
