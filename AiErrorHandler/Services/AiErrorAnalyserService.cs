using AiErrorHandler.Domain;
using AiErrorHandler.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using System.Text;

namespace AiErrorHandler.Core;

internal class AiErrorAnalyserService : IAiErrorAnalyzer
{
    private readonly IOptions<AiAnalyzerOptions> _options;
    private readonly ILogger<AiErrorAnalyserService> _logger;

    private readonly ChatClient _chatClient;

    public AiErrorAnalyserService(IOptions<AiAnalyzerOptions> options, ILogger<AiErrorAnalyserService> logger)
    {
        _options = options;
        _logger = logger;
        _chatClient = new ChatClient(_options.Value.Model, _options.Value.Key);
    }

    public async Task ExceptionAnalyse(Exception exception)
    {
        if (_options.Value.IsWriteToLogger)
        {
            _logger.LogInformation("Start AI Analyser");
        }
        else
        {
            Console.WriteLine("Start AI Analyser");
        }
        var listMessage = new List<ChatMessage>
        {
            ChatMessage.CreateSystemMessage(Prompt.BasePrompt)
        };

        if (_options.Value.IsRussianLanguage)
        {
            listMessage.Add(ChatMessage.CreateSystemMessage(Prompt.RussianLanguagePrompt));
        }

        listMessage.Add(ChatMessage.CreateUserMessage(exception.Message, exception.StackTrace ?? string.Empty, exception.Source));

        var stringBuilder = new StringBuilder();
        var updates = _chatClient.CompleteChatStreamingAsync(listMessage);

        await foreach (var update in updates)
        {
            foreach (var updatePart in update.ContentUpdate)
            {
                stringBuilder.Append(updatePart.Text);
            }
        }

        string messageForUser = GenerateMessageForUser(stringBuilder.ToString());

        if (_options.Value.IsWriteToLogger)
        {
            _logger.LogInformation(messageForUser);
        }
        else
        {
            Console.WriteLine(messageForUser);
        }
    }

    private string GenerateMessageForUser(string message)
    {
        var builder = new StringBuilder();
        builder.AppendLine("--------------------------------");
        builder.AppendLine("AI EXCEPTION ANALYSIS");
        builder.AppendLine("--------------------------------");

        builder.AppendLine(message);

        builder.AppendLine("--------------------------------");

        return builder.ToString();
    }
}
