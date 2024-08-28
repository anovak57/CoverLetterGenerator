using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.DTOs;
using src.Interfaces;
using src.Models;

namespace src.Controllers;
public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserInstructionsRepository _instructionRepository;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserInstructionsRepository instructionsRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _instructionRepository = instructionsRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new AppUser { UserName = registerModel.Email, Email = registerModel.Email };
        var result = await _userManager.CreateAsync(user, registerModel.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var userInstructions = new CustomUserInstructions() { UserId = user.Id, Instructions = "" };
        _instructionRepository.AddAsync(userInstructions);

        int instructionsResult = await _instructionRepository.SaveChangesAsync();

        if (instructionsResult <= 0)
        {
            return BadRequest("Error saving user instructions");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return Ok("Registration successful");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
        
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid login attempt");
        }

        return Ok("Login successful");
    }

    [HttpPost("logout")]
    [Authorize(Policy = "RequireAuthenticatedUser")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logout successful");
    }
}