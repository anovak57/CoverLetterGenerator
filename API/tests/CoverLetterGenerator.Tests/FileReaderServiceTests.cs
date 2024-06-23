using System.Text.Json;

namespace CoverLetterGenerator.Tests
{
    public class FileReaderServiceTests
    {
        private FileReaderService _fileReaderService;

        [SetUp]
        public void Setup()
        {
            _fileReaderService = new FileReaderService();
        }

        [Test]
        public async Task ReadFileAsync_ValidPath_ReturnsExpectedObject()
        {
            var tempFilePath = Path.GetTempFileName();
            var expectedInstructions = new CoverLetterInstructionsDto
            {
                GeneralInstructions = "Follow these general instructions",
                Structure = "More detailed structure instructions"
            };
            var json = JsonSerializer.Serialize(expectedInstructions);
            
            try
            {
                File.WriteAllText(tempFilePath, json);

                var result = await _fileReaderService.ReadFileAsync<CoverLetterInstructionsDto>(tempFilePath);

                Assert.That(result.GeneralInstructions, Is.EqualTo(expectedInstructions.GeneralInstructions));
                Assert.That(result.Structure, Is.EqualTo(expectedInstructions.Structure));
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
