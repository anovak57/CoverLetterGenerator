using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CoverLetter> CoverLetters { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CoverLetter>()
            .HasOne(cl => cl.User)
            .WithMany(u => u.CoverLetters)
            .HasForeignKey(cl => cl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
