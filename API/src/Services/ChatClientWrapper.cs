using OpenAI.Chat;
using src.Interfaces;

namespace src.Services;

public class ChatClientWrapper : IChatClient
{
    private readonly ChatClient _client;

    public ChatClientWrapper(string model, string apiKey)
    {
        _client = new ChatClient(model, apiKey);
    }

    public async Task<string> CompleteChatAsync(string prompt)
    {
        ChatCompletion completion = await _client.CompleteChatAsync(prompt);
        return completion.ToString();
    }
}