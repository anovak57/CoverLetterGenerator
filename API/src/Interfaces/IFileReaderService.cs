namespace src.Interfaces;

public interface IFileReaderService
{
    Task<T> ReadFileAsync<T>(string path);
}