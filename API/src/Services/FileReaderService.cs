using System.Text.Json;
using src.Interfaces;

namespace src.Services;

public class FileReaderService : IFileReaderService
{
    public async Task<T> ReadFileAsync<T>(string filePath)
    {
        string jsonString = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<T>(jsonString);
    } 
}