using src.DTOs;
using src.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace src.Controllers;

[Authorize(Policy = "RequireAuthenticatedUser")]
public class CoverLetterController : BaseApiController
{

    private readonly ICoverLetterService _coverLetterService;
    private readonly ICoverLetterRepository _coverLetterRepository;
    private readonly IUserInstructionsRepository _instructionRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public CoverLetterController(
        ICoverLetterService coverLetterService,
        ICoverLetterRepository coverLetterRepository,
        IUserInstructionsRepository instructionsRepository,
        IMapper mapper,
        UserManager<AppUser> userManager)
    {
        _coverLetterService = coverLetterService;
        _coverLetterRepository = coverLetterRepository;
        _instructionRepository = instructionsRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost("generate")]
    [AllowAnonymous]
    public async Task<ActionResult<CoverLetterResponseDto>> GenerateCoverLetter([FromBody] CoverLetterRequestDto request)
    {
        if (request is null)
        {
            return BadRequest("Invalid input");
        }

        var user = await _userManager.GetUserAsync(User);
        var instructions = user.UserInstructions.Instructions;

        var coverLetter = await _coverLetterService.GenerateCoverLetterAsync(request.JobListing, request.Experience, instructions);

        return Ok(coverLetter);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoverLetterDto>> GetCoverLetterById(int id)
    {
        var coverLetter = await _coverLetterRepository.GetCoverLetterById(id);

        if (coverLetter is null)
        {
            return NotFound("Requested cover letter does not exist");
        }

        var coverLetterDtos = _mapper.Map<CoverLetterDto>(coverLetter);

        return Ok(coverLetterDtos);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoverLetterDto>>> GetCoverLetters()
    {
        var user = await _userManager.GetUserAsync(User);

        var coverLetters = await _coverLetterRepository.GetCoverLetters(user.Id);

        var coverLetterDtos = _mapper.Map<IEnumerable<CoverLetterDto>>(coverLetters);

        return Ok(coverLetterDtos);
    }


    [HttpPost("save")]
    public async Task<ActionResult<CoverLetterDto>> SaveCoverLetter([FromBody] CoverLetterDto coverLetterDto)
    {

        var coverLetter = _mapper.Map<CoverLetter>(coverLetterDto);
        var user = await _userManager.GetUserAsync(User);

        coverLetter.UserId = user.Id;

        _coverLetterRepository.AddCoverLetter(coverLetter);

        int result = await _coverLetterRepository.SaveChangesAsync();

        if (result > 0)
        {
            return Ok(coverLetterDto);
        }

        return BadRequest("Failed to create CoverLetter.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCoverLetter(int id)
    {
        var success = await _coverLetterRepository.RemoveCoverLetterById(id);

        if (success)
        {
            return NoContent();
        }

        return BadRequest("Failed to delete cover letter");
    }

    [HttpGet("instructions")]
    public async Task<ActionResult<UserInstructionsDto>> GetUserInstructions()
    {
        var user = await _userManager.GetUserAsync(User);

        var instructions = await _instructionRepository.GetByUserIdAsync(user.Id);

        var instructionsDto = _mapper.Map<UserInstructionsDto>(instructions);

        return Ok(instructionsDto);
    }

    [HttpPut("instructions/save")]
    public async Task<ActionResult<UserInstructionsDto>> UpdateUserInstructions([FromBody] UserInstructionsDto userInstructionsDto)
    {
        var user = await _userManager.GetUserAsync(User);

        var instructions = userInstructionsDto.Instructions;

        _instructionRepository.UpdateAsync(user.Id, instructions);

        int result = await _instructionRepository.SaveChangesAsync();

        if (result > 0)
        {
            return NoContent();
        }

        return BadRequest("Error saving the user instructions");
    }
}
