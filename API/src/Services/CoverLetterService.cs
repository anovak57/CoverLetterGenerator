using src.DTOs;
using src.Interfaces;

namespace src.Services;

public class CoverLetterService : ICoverLetterService
{
    private readonly IFileReaderService _reader;
    private readonly IChatClient _chatClient;
    private readonly IConfiguration _configuration;

    public CoverLetterService(IConfiguration configuration, IFileReaderService reader, IChatClient chatClient)
    {
        _reader = reader;
        _chatClient = chatClient;
        _configuration = configuration;
    }

    public async Task<CoverLetterResponseDto> GenerateCoverLetterAsync(string jobListing, string experience, string userInstructions)
    {
        if (string.IsNullOrWhiteSpace(jobListing))
        {
            throw new ArgumentException("Job listing is required.", nameof(jobListing));
        }

        if (string.IsNullOrWhiteSpace(experience))
        {
            throw new ArgumentException("Experience is required.", nameof(experience));
        }

        CoverLetterInstructionsDto defaultInstructions = await GetInstructionsAsync();

        string gptPrompt = BuildGptPrompt(jobListing, experience, defaultInstructions, userInstructions);

        string gptAnswer = await _chatClient.CompleteChatAsync(gptPrompt);

        return new CoverLetterResponseDto { CoverLetter = gptAnswer };
    }

    private async Task<CoverLetterInstructionsDto> GetInstructionsAsync()
    {
        return await _reader.ReadFileAsync<CoverLetterInstructionsDto>(_configuration["CoverLetterConfigFilePath"]);
    }

    private static string BuildGptPrompt(string jobListing, string experience, CoverLetterInstructionsDto instructions, string userInstructions)
    {
        return $"Generate a cover letter for a job listing: {jobListing}. Experience: {experience}. " +
               $"Follow these general instructions: {instructions.GeneralInstructions}. " +
               $"More detailed instructions: {instructions.Structure}" +
               $"Extra instructions, Make sure to put this as high priority: {userInstructions}";
    }
}
