using AiErrorHandler.Core;
using Microsoft.AspNetCore.Http;

namespace AiErrorHandler.AspNetCore;

public class AiErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await AiErrorStaticAnalyse.DescribeException(e);
            throw;
        }
    }
}
