namespace AiErrorHandler.Models;

internal interface IAiErrorAnalyzer
{
    public Task ExceptionAnalyse(Exception exception);
}
