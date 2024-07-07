using Microsoft.EntityFrameworkCore;
using src.Interfaces;
using src.Models;

namespace src.Data;

public class CoverLetterRepository : ICoverLetterRepository
{
    private readonly AppDbContext _context;

    public CoverLetterRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddCoverLetter(CoverLetter coverLetter)
    {
        _context.CoverLetters.Add(coverLetter);
    }

    public void RemoveCoverLetter(CoverLetter coverLetter)
    {
        _context.CoverLetters.Remove(coverLetter);
    }

    public async Task<IEnumerable<CoverLetter>> GetCoverLetters(string userId)
    {
        return await _context.CoverLetters
                            .Where(cl => cl.UserId == userId)
                            .ToListAsync();
    }

    public async Task<CoverLetter> GetCoverLetterById(int id)
    {
        return await _context.CoverLetters.FindAsync(id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
