using Microsoft.AspNetCore.Identity;

namespace src.Models;

public class AppUser : IdentityUser
{
    public ICollection<CoverLetter> CoverLetters { get; set; }
    public CustomUserInstructions UserInstructions { get; set; }
}