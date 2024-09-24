using src.DTOs;
using src.Models;

namespace src.Interfaces;

public interface ICoverLetterService
{
    Task<CoverLetterResponseDto> GenerateCoverLetterAsync(string jobListing, string experience, string userInstructions);
}
