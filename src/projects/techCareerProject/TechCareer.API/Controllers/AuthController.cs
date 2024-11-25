using Core.Security.Dtos;
using Microsoft.AspNetCore.Mvc;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _authService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserForLoginDto dto,CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(dto, cancellationToken);
        return Ok(result);
    }
}