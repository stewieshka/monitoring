using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Monitoring.App.Dtos;
using Monitoring.App.Dtos.Auth;
using Monitoring.App.Services;

namespace Monitoring.API.Controllers;

[Route("api/[controller]")]
public class AuthController : CustomControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var authResponse = await _authService.RegisterAsync(dto);

        return authResponse.Match(
            success => Ok(success),
            errors => Problem(errors[0]));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var authResponse = await _authService.LoginAsync(dto);

        return authResponse.Match(
            success => Ok(success),
            errors => Problem(errors[0]));
    }
}