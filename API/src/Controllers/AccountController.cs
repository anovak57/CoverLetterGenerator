using CoverLetterGeneratorAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.DTOs;
using src.Models;

namespace src.Controllers;
public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerModel)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = registerModel.Email, Email = registerModel.Email };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Registration successful");
            }
            return BadRequest(result.Errors);
        }
        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok("Login successful");
            }
            return Unauthorized("Invalid login attempt");
        }
        return BadRequest(ModelState);
    }

    [HttpPost("logout")]
    [Authorize(Policy = "RequireAuthenticatedUser")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logout successful");
    }
}