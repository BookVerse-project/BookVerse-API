using System;
using Microsoft.AspNetCore.Mvc;
using BookVerse.Application.InterfaceServices;
using BookVerse.Application.DTOs.Auth;
using BookVerse.Application.Common;

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
        return Ok(ApiResponse.Success());
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
    var response = ApiResponse<LoginResponse>.Success(await _service.LoginAsync(request));

    if (!response.Succeeded)
        return BadRequest(response); 

    return Ok(ApiResponse<LoginResponse>.Success(response.Result)); 
    }
}}