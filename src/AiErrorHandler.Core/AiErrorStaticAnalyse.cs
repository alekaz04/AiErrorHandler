using AiErrorHandler.Models;

namespace AiErrorHandler.Core;

public static class AiErrorStaticAnalyse
{
    private static OpenAiClient? _openAiClient;

    public static async Task DescribeException(Exception ex)
    {
        if (_openAiClient is null)
        {
            throw new AiErrorHandlerException("OpenAiClient is not initialized");
        }

        string analyse = await _openAiClient.GetErrorDescription(ex);
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(analyse);
        Console.WriteLine("-----------------------------------");
    }

    public static void InitializeGpt(ConfigureAiHandler options)
    {
        _openAiClient = new OpenAiClient(options.Model, options.Key);
    }
}
