using System.Text.Json;
using CoverLetterGeneratorAPI.DTOs;
using CoverLetterGeneratorAPI.Interfaces;
using OpenAI.Chat;

namespace CoverLetterGeneratorAPI.Services
{
    public class CoverLetterService : ICoverLetterService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiModel;
        private readonly string _apiKey;
        private CoverLetterInstructionsDto _instructions;

        public CoverLetterService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiModel = _configuration["OpenAI:GptModel"];
            _apiKey = _configuration["OpenAI:ApiKey"];

            LoadCoverLetterInstructions();
        }

        private void LoadCoverLetterInstructions()
        {
            string jsonFilePath = _configuration["CoverLetterConfigFilePath"];
            string jsonString = File.ReadAllText(jsonFilePath);
            _instructions = JsonSerializer.Deserialize<CoverLetterInstructionsDto>(jsonString);
        }

        public async Task<String> GenerateCoverLetterAsync(string jobListing, string experience)
        {
            string GptPrompt = $"Generate a cover letter for a job listing: {jobListing}. Experience: {experience}. " +
                               $"Follow these general instructions: {_instructions.GeneralInstructions}. " +
                               $"More detailed instructions: {_instructions.Structure}";

            ChatClient client = new(model: _apiModel, _apiKey);

            ChatCompletion completion = await Task.Run(() => client.CompleteChat(GptPrompt));
            
            return completion.ToString();
        }
    }
}
