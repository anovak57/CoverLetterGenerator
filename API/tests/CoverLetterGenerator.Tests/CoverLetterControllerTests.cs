using System.Security.Claims;
using AutoMapper;
using CoverLetterGeneratorAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace CoverLetterGenerator.Tests
{
    public class CoverLetterControllerTests
    {
        private Mock<ICoverLetterService> _mockCoverLetterService;
        private Mock<ICoverLetterRepository> _mockCoverLetterRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<UserManager<AppUser>> _mockUserManager;
        private CoverLetterController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCoverLetterService = new Mock<ICoverLetterService>();
            _mockCoverLetterRepository = new Mock<ICoverLetterRepository>();
            _mockMapper = new Mock<IMapper>();

            var store = new Mock<IUserStore<AppUser>>();
            _mockUserManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new CoverLetterController(
                _mockCoverLetterService.Object,
                _mockCoverLetterRepository.Object,
                _mockMapper.Object,
                _mockUserManager.Object
            );

            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, "testUserId")
            ], "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Test]
        public async Task GenerateCoverLetter_InvalidRequest_ReturnsBadRequest()
        {
            var result = await _controller.GenerateCoverLetter(null);

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GenerateCoverLetter_ValidRequest_ReturnsOk()
        {
            var request = new CoverLetterRequestDto { JobListing = "Job", Experience = "Experience" };
            var response = new CoverLetterResponseDto { CoverLetter = "CoverLetterContent" };

            _mockCoverLetterService.Setup(s => s.GenerateCoverLetterAsync(request.JobListing, request.Experience))
                .ReturnsAsync(response.CoverLetter);

            var result = await _controller.GenerateCoverLetter(request);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(response.CoverLetter));
        }

        [Test]
        public async Task GetCoverLetterById_CoverLetterNotFound_ReturnsNotFound()
        {
            _mockCoverLetterRepository.Setup(r => r.GetCoverLetterById(1)).ReturnsAsync((CoverLetter)null);

            var result = await _controller.GetCoverLetterById(1);

            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetCoverLetterById_CoverLetterFound_ReturnsOk()
        {
            var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };
            var coverLetterDto = new CoverLetterDto {};

            _mockCoverLetterRepository.Setup(r => r.GetCoverLetterById(1)).ReturnsAsync(coverLetter);
            _mockMapper.Setup(m => m.Map<CoverLetterDto>(coverLetter)).Returns(coverLetterDto);

            var result = await _controller.GetCoverLetterById(1);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(coverLetterDto));
        }

        [Test]
        public async Task GetCoverLetters_ReturnsCoverLettersForUser()
        {
            var user = new AppUser { Id = "testUserId" };
            var coverLetters = new List<CoverLetter>
            {
                new CoverLetter { Id = 1, UserId = "testUserId" },
                new CoverLetter { Id = 2, UserId = "testUserId" }
            };
            var coverLetterDtos = new List<CoverLetterDto>
            {
                new() { },
                new() { }
            };

            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockCoverLetterRepository.Setup(r => r.GetCoverLetters(user.Id)).ReturnsAsync(coverLetters);
            _mockMapper.Setup(m => m.Map<IEnumerable<CoverLetterDto>>(coverLetters)).Returns(coverLetterDtos);

            var result = await _controller.GetCoverLetters();

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(coverLetterDtos));
        }

        [Test]
        public async Task SaveCoverLetter_ValidCoverLetter_SavesAndReturnsOk()
        {
            var user = new AppUser { Id = "testUserId" };
            var coverLetterDto = new CoverLetterDto {};
            var coverLetter = new CoverLetter { Id = 1, UserId = "testUserId" };

            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<CoverLetter>(coverLetterDto)).Returns(coverLetter);
            _mockCoverLetterRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _controller.SaveCoverLetter(coverLetterDto);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(coverLetterDto));
        }

        [Test]
        public async Task SaveCoverLetter_SaveFails_ReturnsBadRequest()
        {
            var user = new AppUser { Id = "testUserId" };
            var coverLetterDto = new CoverLetterDto {};
            var coverLetter = new CoverLetter { Id = 1, UserId = "testUserId" };

            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<CoverLetter>(coverLetterDto)).Returns(coverLetter);
            _mockCoverLetterRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _controller.SaveCoverLetter(coverLetterDto);

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteCoverLetter_CoverLetterNotFound_ReturnsBadRequest()
        {
            _mockCoverLetterRepository.Setup(r => r.GetCoverLetterById(1)).ReturnsAsync((CoverLetter)null);

            var result = await _controller.DeleteCoverLetter(1);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task DeleteCoverLetter_DeleteSuccessful_ReturnsNoContent()
        {
            var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };

            _mockCoverLetterRepository.Setup(r => r.GetCoverLetterById(1)).ReturnsAsync(coverLetter);
            _mockCoverLetterRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _controller.DeleteCoverLetter(1);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteCoverLetter_DeleteFails_ReturnsBadRequest()
        {
            var coverLetter = new CoverLetter { Id = 1, UserId = "user1" };

            _mockCoverLetterRepository.Setup(r => r.GetCoverLetterById(1)).ReturnsAsync(coverLetter);
            _mockCoverLetterRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

            var result = await _controller.DeleteCoverLetter(1);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}
