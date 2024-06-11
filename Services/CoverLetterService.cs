using CoverLetterGeneratorAPI.Interfaces;
using OpenAI_API;

namespace CoverLetterGeneratorAPI.Services
{
    public class CoverLetterService : ICoverLetterService
    {
        private readonly IConfiguration _configuration;

        public CoverLetterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<String> GenerateCoverLetterAsync(string jobListing, string experience)
        {
            var apiModel = _configuration["OpenAI:GptModel"];

            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(_configuration["OpenAI:ApiKey"]));
            var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            {
                Prompt = $"Generate a cover letter for a job listing: {jobListing}. Experience: {experience}.",
                Model = apiModel,
                Temperature = 0.2,
                MaxTokens = 200
            };
            var result = await api.Completions.CreateCompletionsAsync(completionRequest, 1);
            return result.ToString();
        }
    }
}
