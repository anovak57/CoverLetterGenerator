using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace CoverLetterGenerator.Tests;

public class CoverLetterRepositoryTests
{
    private AppDbContext _context;
    private ICoverLetterRepository _coverLetterRepository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CoverLetterDb")
            .Options;

        _context = new AppDbContext(options);
        _coverLetterRepository = new CoverLetterRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public void AddCoverLetter_ValidCoverLetter_AddsCoverLetterToContext()
    {
        var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };
        _coverLetterRepository.AddCoverLetter(coverLetter);

        var addedCoverLetter = _context.CoverLetters.Find(1);
        Assert.That(addedCoverLetter, Is.Not.Null);
        Assert.That(addedCoverLetter.UserId, Is.EqualTo(coverLetter.UserId));
    }

    [Test]
    public async Task RemoveCoverLetter_ValidCoverLetter_RemovesCoverLetterFromContext()
    {
        var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };
        _context.CoverLetters.Add(coverLetter);
        await _context.SaveChangesAsync();

        _coverLetterRepository.RemoveCoverLetter(coverLetter);
        await _coverLetterRepository.SaveChangesAsync();

        var removedCoverLetter = await _context.CoverLetters.FindAsync(1);
        Assert.That(removedCoverLetter, Is.Null);
    }


    [Test]
    public async Task GetCoverLetters_ValidUserId_ReturnsCoverLettersForUser()
    {
        var data = new List<CoverLetter>
        {
            new() { Id = 1, UserId = "user1" },
            new() { Id = 2, UserId = "user2" },
            new() { Id = 3, UserId = "user1" }
        };
        _context.CoverLetters.AddRange(data);
        _context.SaveChanges();

        var result = await _coverLetterRepository.GetCoverLetters("user1");
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(cl => cl.Id == 1), Is.True);
            Assert.That(result.Any(cl => cl.Id == 3), Is.True);
        });
    }

    [Test]
    public async Task GetCoverLetterById_ValidId_ReturnsCorrectCoverLetter()
    {
        var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };
        _context.CoverLetters.Add(coverLetter);
        _context.SaveChanges();

        var result = await _coverLetterRepository.GetCoverLetterById(1);

        Assert.That(result, Is.EqualTo(coverLetter));
    }

    [Test]
    public async Task SaveChangesAsync_WhenCalled_CallsSaveChangesAsyncOnContext()
    {
        var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };
        _coverLetterRepository.AddCoverLetter(coverLetter);

        var result = await _coverLetterRepository.SaveChangesAsync();

        Assert.That(result, Is.EqualTo(1));
        var savedCoverLetter = _context.CoverLetters.Find(1);
        Assert.That(savedCoverLetter, Is.Not.Null);
    }
}
