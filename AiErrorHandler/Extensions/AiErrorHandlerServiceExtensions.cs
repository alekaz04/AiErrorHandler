using AiErrorHandler.Core;
using AiErrorHandler.Domain;
using AiErrorHandler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AiErrorHandler.AspNetCore.Extensions;

public static class AiErrorHandlerServiceExtensions
{
    public static IServiceCollection AddAiErrorHandler(this IServiceCollection services, Action<AiAnalyzerOptions> configure)
    {
        services.AddScoped<AiErrorHandlerMiddleware>();
        services.AddScoped<IAiErrorAnalyzer, AiErrorAnalyserService>();

        services.Configure(configure);

        return services;
    }

    public static IApplicationBuilder UseAiErrorHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AiErrorHandlerMiddleware>();

        return app;
    }
}
