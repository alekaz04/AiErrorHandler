using AiErrorHandler.Core.Promts;
using OpenAI.Chat;
using System.Text;

namespace AiErrorHandler.Core;

public class OpenAiClient
{
    private readonly ChatClient _client;

    private readonly string _systemPrompt = Promt.BasePrompt;

    public OpenAiClient(string model, string key)
    {
        _client = new ChatClient(model, key);
    }

    public async Task<string> GetErrorDescription(Exception ex)
    {
        string prompt = string.Format(_systemPrompt, ex.Message, ex.StackTrace, ex.HelpLink, ex.Source, ex.TargetSite);

        var basePrompt = ChatMessage.CreateSystemMessage(prompt);

        var russianAdditional = ChatMessage.CreateSystemMessage(Promt.RussianLanguagePrompt);

        var exceptionPrompt = ChatMessage.CreateUserMessage(ex.Source, ex.StackTrace ?? string.Empty, ex.Message, ex.HelpLink ?? string.Empty);

        var stringBuilder = new StringBuilder();
        var updates = _client.CompleteChatStreamingAsync(basePrompt, russianAdditional, exceptionPrompt);

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
