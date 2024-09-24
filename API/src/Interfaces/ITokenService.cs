using System.Security.Claims;
using src.DTOs;
using src.Models;

namespace src.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}