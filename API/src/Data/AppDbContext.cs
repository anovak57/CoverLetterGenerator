using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CoverLetter> CoverLetters { get; set; }
    }
}
