using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Dtos;
using Pustok.Business.Services.Abstractions;
using System.Security.Claims;

namespace Pustok.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _service) : ControllerBase
{


    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _service.RegisterAsync(dto);
        return Ok(result);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        var result = await _service.LoginAsync(dto);
        return Ok(result);
    }





}
