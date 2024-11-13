using CineRepository.Models.DTO;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
  {
  [Route("api/[controller]")]
  [ApiController]
  public class UserAchievementsController : ControllerBase
    {
    private readonly IUserAchievementsService _achievementsService;

    public UserAchievementsController(IUserAchievementsService achievementsService)
      {
      _achievementsService = achievementsService;
      }

    [HttpPost("AchievementUser")]

    public async Task<IActionResult> CreateAchievement([FromBody] UserAchievementPostRequestDTO userAchievement)
      {
      try
        {

        if (!await _achievementsService.UserIdExistsAsync(Convert.ToInt32(userAchievement.UserId)))
          {
          return BadRequest($"User with ID {userAchievement.UserId} not found");
          }

        if (!await _achievementsService.AchievementExistsAsync(Convert.ToInt32(userAchievement.AchievementId)))
          {
          return BadRequest($"Achievement with ID {userAchievement.AchievementId} not found");
          }

        var result = await _achievementsService.CreateAchievementAsync(userAchievement);
        return Ok("creado exitosamente");
        }
      catch (Exception ex)
        {
        return StatusCode(500, "An internal error occurred." + ex.Message);
        }
      }

    [HttpGet("AchievementUser")]

    public async Task<IActionResult> GetByUsernameAsync([FromQuery] int userId)
      {
      try
        {
        if (userId == 0)
          {
          return BadRequest("User id cannot be empty.");
          }

        if (!await _achievementsService.UserIdExistsAsync(userId))
          {
          return NotFound($"User '{userId}' not found.");
          }

        var achievement = await _achievementsService.GetAchievementByUserIdAsync(userId);
        if (achievement == null)
          {
          return NotFound($"No achievements found for user '{userId}'.");
          }

        return Ok(achievement);
        }
      catch (Exception ex)
        {
        return StatusCode(500, "An internal server error occurred." + ex.Message);
        }
      }

    }
  }
