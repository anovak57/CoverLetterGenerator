namespace CoverLetterGeneratorAPI.Interfaces
{
    public interface ICoverLetterService
    {
        Task<String> GenerateCoverLetterAsync(string jobListing, string experience);
    }
}
