using AiErrorHandler.Domain;
using AiErrorHandler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AiErrorHandler.AspNetCore;

internal class AiErrorHandlerMiddleware : IMiddleware
{
    private readonly IOptions<AiAnalyzerOptions> _options;
    private readonly IAiErrorAnalyzer _service;

    public AiErrorHandlerMiddleware(IOptions<AiAnalyzerOptions> options, IAiErrorAnalyzer service)
    {
        _options = options;
        _service = service;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException ex)
        {
            throw;
        }
        catch (Exception e)
        {
            if (_options.Value.ProcessInBackground)
            {
                try
                {
                    _ = Task.Run(async () =>
                    {
                        await _service.ExceptionAnalyse(e);
                    });
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            else
            {
                try
                {
                    await _service.ExceptionAnalyse(e);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            throw;
        }
    }
}
