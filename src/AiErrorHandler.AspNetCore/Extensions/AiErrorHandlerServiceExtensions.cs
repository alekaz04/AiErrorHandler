using AiErrorHandler.Core;
using AiErrorHandler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AiErrorHandler.AspNetCore;

public static class AiErrorHandlerServiceExtensions
{
    public static IServiceCollection AddAiErrorHandler(this IServiceCollection services)
    {
        services.AddScoped<AiErrorHandlerMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseAiErrorHandlerMiddleware(this IApplicationBuilder app, ConfigureAiHandler options)
    {
        AiErrorStaticAnalyse.InitializeGpt(options);
        app.UseMiddleware<AiErrorHandlerMiddleware>();

        return app;
    }
}
