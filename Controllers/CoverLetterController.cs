using CoverLetterGeneratorAPI.Interfaces;
using CoverLetterGeneratorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoverLetterGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoverLetterController : ControllerBase
    {

        private readonly ICoverLetterService _coverLetterService;

        public CoverLetterController(ICoverLetterService coverLetterService)
        {
            _coverLetterService = coverLetterService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<String>> GenerateCoverLetter([FromBody] CoverLetterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid input");
            }

            var coverLetter = await _coverLetterService.GenerateCoverLetterAsync(request.JobListing, request.Experience);

            return Ok(coverLetter);
        }
    }
}
