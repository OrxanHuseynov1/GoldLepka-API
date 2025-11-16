using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserGetDto>> Login(UserLoginDto dto)
    {
        var user = await _service.LoginAsync(dto);
        if (user == null) return Unauthorized();
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserGetDto>>> GetAll()
    {
        var users = await _service.GetAllAsync();
        return Ok(users);
    }
}
