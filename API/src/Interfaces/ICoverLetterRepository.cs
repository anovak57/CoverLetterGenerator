using src.Models;

namespace src.Interfaces;

public interface ICoverLetterRepository
{
    void AddCoverLetter(CoverLetter coverLetter);
    Task<bool> RemoveCoverLetterById(int id);
    Task<IEnumerable<CoverLetter>> GetCoverLetters(string userId);
    Task<CoverLetter> GetCoverLetterById(int id);
    Task<int> SaveChangesAsync();
}