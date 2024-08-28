using src.Models;

namespace src.Interfaces;

public interface IUserInstructionsRepository
{
    Task<CustomUserInstructions> GetByUserIdAsync(string userId);
    void UpdateAsync(string userId, string userInstructions);
    void AddAsync(CustomUserInstructions userInstructions);
    Task<bool> RemoveUserInstructionsAsync(string id);
    Task<int> SaveChangesAsync();
}