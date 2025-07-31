using AiErrorHandler.Domain;
using AiErrorHandler.Models;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using System.Text;

namespace AiErrorHandler.Core;

internal class OpenAiClient : IAiClient
{
    private readonly ChatClient _client;

    public OpenAiClient(IOptions<AiAnalyzerOptions> options)
    {
        _client = new ChatClient(options.Value.Model, options.Value.Key);
    }

    public async Task<string> GetErrorDescription(List<string> messages, CancellationToken cancellationToken)
    {
        var chatMessages = messages.Select(ChatMessage.CreateSystemMessage)
            .ToList<ChatMessage>();

        var stringBuilder = new StringBuilder();
        var updates = _client.CompleteChatStreamingAsync(chatMessages, cancellationToken: cancellationToken);

        await foreach (var update in updates)
        {
            foreach (var updatePart in update.ContentUpdate)
            {
                stringBuilder.Append(updatePart.Text);
            }
        }

        return stringBuilder.ToString();
    }
}
