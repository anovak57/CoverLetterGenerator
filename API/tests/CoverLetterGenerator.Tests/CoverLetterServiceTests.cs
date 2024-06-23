namespace CoverLetterGenerator.Tests
{
    public class CoverLetterServiceTests
    {
        private Mock<IFileReaderService> _fileReader;
        private Mock<IChatClient> _chatClient;
        private ICoverLetterService _coverLetterService;
        private Mock<IConfiguration> _configuration;
        private CoverLetterInstructionsDto _instructions;

        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReaderService>();
            _instructions = new CoverLetterInstructionsDto
            {
                GeneralInstructions = "Follow these general instructions",
                Structure = "More detailed structure instructions"
            };

            _fileReader.Setup(r =>
                r.ReadFileAsync<CoverLetterInstructionsDto>("mocked/path/to/config.json"))
                .ReturnsAsync(_instructions);

            _chatClient = new Mock<IChatClient>();
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c["CoverLetterConfigFilePath"]).Returns("mocked/path/to/config.json");

            _coverLetterService = new CoverLetterService(
                _configuration.Object,
                _fileReader.Object,
                _chatClient.Object);
        }

        [Test]
        public void GenerateCoverLetterAsync_JobListingIsNullOrWhiteSpace_ThrowsNewArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await _coverLetterService.GenerateCoverLetterAsync("", "experience"));

            Assert.That(ex.Message, Does.Contain("Job listing is required."));
        }

        [Test]
        public void GenerateCoverLetterAsync_ExperienceIsNullOrWhiteSpace_ThrowsNewArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await _coverLetterService.GenerateCoverLetterAsync("jobListing", ""));

            Assert.That(ex.Message, Does.Contain("Experience is required."));
        }

        [Test]
        public async Task GenerateCoverLetterAsync_ArgumentsPassedCorrectly_ReturnsCoverLetterAsString()
        {
            string jobListing = "Software Developer at Company XYZ";
            string experience = "5 years of experience in software development";
            string expectedCoverLetter = "Dear Hiring Manager, ...";

            _chatClient.Setup(c => c.CompleteChatAsync(It.IsAny<string>()))
                       .ReturnsAsync(expectedCoverLetter);

            string actualCoverLetter = await _coverLetterService.GenerateCoverLetterAsync(jobListing, experience);

            Assert.That(actualCoverLetter, Is.EqualTo(expectedCoverLetter));
        }
    }
}
