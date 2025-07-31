namespace AiErrorHandler.Models;

internal interface IAiClient
{
    public Task<string> GetErrorDescription(List<string> messages, CancellationToken cancellationToken);
}
