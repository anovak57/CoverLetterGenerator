namespace src.Interfaces;

public interface IChatClient
{
    Task<string> CompleteChatAsync(string prompt);
}