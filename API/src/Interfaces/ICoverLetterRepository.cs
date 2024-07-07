using src.Models;

namespace src.Interfaces;

public interface ICoverLetterRepository
{
    void AddCoverLetter(CoverLetter coverLetter);
    void RemoveCoverLetter(CoverLetter coverLetter);
    Task<IEnumerable<CoverLetter>> GetCoverLetters();
    Task<CoverLetter> GetCoverLetterById(int id);
    Task<int> SaveChangesAsync();
}