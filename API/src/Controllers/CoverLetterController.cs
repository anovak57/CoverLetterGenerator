using src.DTOs;
using src.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using src.Models;

namespace CoverLetterGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoverLetterController : ControllerBase
    {

        private readonly ICoverLetterService _coverLetterService;
        private readonly ICoverLetterRepository _coverLetterRepository;
        private readonly IMapper _mapper;

        public CoverLetterController(ICoverLetterService coverLetterService, ICoverLetterRepository coverLetterRepository, IMapper mapper)
        {
            _coverLetterService = coverLetterService;
            _coverLetterRepository = coverLetterRepository;
            _mapper = mapper;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<CoverLetterResponseDto>> GenerateCoverLetter([FromBody] CoverLetterRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid input");
            }

            var coverLetter = await _coverLetterService.GenerateCoverLetterAsync(request.JobListing, request.Experience);

            return Ok(coverLetter);
        }


        [HttpPost("save")]
        public async Task<ActionResult<CoverLetterDto>> SaveCoverLetter([FromBody] CoverLetterDto coverLetterDto)
        {

            var coverLetter = _mapper.Map<CoverLetter>(coverLetterDto);

            _coverLetterRepository.AddCoverLetter(coverLetter);

            int result = await _coverLetterRepository.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(coverLetterDto);
            }

            return BadRequest("Failed to create CoverLetter.");
        }
    }
}
