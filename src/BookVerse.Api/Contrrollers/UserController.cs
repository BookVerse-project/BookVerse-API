using System;
using Microsoft.AspNetCore.Mvc;
using BookVerse.Application.InterfaceServices;
using BookVerse.Application.DTOs.Auth;

namespace BookVerse.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _service.RegisterAsync(request);
        return Ok("Register success");
    }
    }   
}