namespace src.Interfaces;

public interface ICoverLetterService
{
    Task<string> GenerateCoverLetterAsync(string jobListing, string experience);
}
