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

    public async void AddCoverLetter(CoverLetter coverLetter)
    {
        await _context.CoverLetters.AddAsync(coverLetter);
    }

    public async Task<bool> RemoveCoverLetterById(int id)
    {
        var coverLetter = await GetCoverLetterById(id);

        if (coverLetter == null)
        {
            return false;
        }

        _context.CoverLetters.Remove(coverLetter);

        return true;
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
