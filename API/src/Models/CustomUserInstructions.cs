using System.ComponentModel.DataAnnotations;

namespace src.Models;

public class CustomUserInstructions
{
    [Key]
    public int Id { get; set; }
    public string Instructions { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}