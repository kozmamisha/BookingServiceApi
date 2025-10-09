using BookingSystemApi.Application.Interfaces;
using BookingSystemApi.Contracts.Auth;
using BookingSystemApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookingSystemApi.Controllers;

[ApiController]
[Route("api/user")]
public class AuthController(IAuthService authService, IOptions<AuthOptions> options) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await authService.Register(request.UserName, request.Email, request.Password, cancellationToken);

        return Created();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var token = await authService.Login(request.Email, request.Password, cancellationToken);
        
        HttpContext.Response.Cookies.Append(options.Value.CookieName, token);

        return Ok();
    }
}