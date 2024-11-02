using CineRepository.Models.Entities;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
  {
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
    {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
      {
      _userService = userService;
      }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAccount newUser)
      {
      if (newUser == null)
        {
        return BadRequest("User cannot be null");
        }
      await _userService.RegisterAsync(newUser);
      return Ok("User registered successfully");


      }
    }
  }
