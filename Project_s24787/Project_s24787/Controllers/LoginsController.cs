using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_s24787.DTOs;
using Project_s24787.Helpers;
using Project_s24787.Services;

namespace Project_s24787.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly IDbService _dbService;

    public LoginsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployee(RegisterRequestDTO registerRequestDto)
    {
        if (await _dbService.DoesUserExist(registerRequestDto.Login))
        {
            return BadRequest($"User with login {registerRequestDto.Login} already in the system");
        } 
        
        await _dbService.RegisterNewEmployee(registerRequestDto);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequestDto)
    {
        var user = await _dbService.GetUser(loginRequestDto.Login);
        if (user is null)
        {
            return NotFound("Invalid login");
        }
        
        var password = user.Password;
        var providedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequestDto.Password, user.Salt);

        if (!password.Equals(providedPassword))
        {
            return Unauthorized("Wrong username or password");
        }

        var tokens = await _dbService.GetTokens(user);

        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto)
    {
        var user = await _dbService.FindUser(refreshTokenRequestDto.RefreshToken);

        if (user is null)
        {
            return NotFound("Invalid refresh token");
        }

        if (SecurityHelpers.IsTokenExpired(refreshTokenRequestDto.RefreshToken))
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        var tokens = await _dbService.GetTokens(user);

        return Ok(tokens);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("role")]
    public async Task<IActionResult> ChangeEmployeesRole(ChangeEmployeeRoleDTO changeEmployeeRole)
    {
        var user = await _dbService.GetUser(changeEmployeeRole.Login);
        if (user is null)
        {
            return NotFound($"User with login {changeEmployeeRole.Login} not found");
        }

        await _dbService.ChangeEmployeesRole(user, changeEmployeeRole.Role);

        return NoContent();
    }
    
}