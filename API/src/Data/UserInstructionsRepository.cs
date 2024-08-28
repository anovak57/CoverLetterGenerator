using Microsoft.EntityFrameworkCore;
using src.Interfaces;
using src.Models;

namespace src.Data;

public class UserInstructionsRepository : IUserInstructionsRepository
{
    private readonly AppDbContext _context;

    public UserInstructionsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomUserInstructions> GetByUserIdAsync(string userId)
    {
        return await _context.CustomUserInstructions
                             .Include(i => i.User)
                             .FirstOrDefaultAsync(i => i.UserId == userId);
    }

    public async void UpdateAsync(string userId, string instructions)
    {
        var existingInstructions = await GetByUserIdAsync(userId);

        existingInstructions.Instructions = instructions;
        _context.CustomUserInstructions.Update(existingInstructions);
    }

    public async void AddAsync (CustomUserInstructions userInstructions)
    {
        await _context.CustomUserInstructions.AddAsync(userInstructions);
    }

    public async Task<bool> RemoveUserInstructionsAsync(string id)
    {
        var userInstructions = await GetByUserIdAsync(id);

        if (userInstructions == null)
        {
            return false;
        }

        _context.CustomUserInstructions.Remove(userInstructions);

        return true;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

}
