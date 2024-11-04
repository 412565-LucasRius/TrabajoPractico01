using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Services.Implementations;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> CreateAchievement([FromBody] UserAchievement userAchievement)
        {
            try
            {

                if (!await _achievementsService.UserExistsAsync(Convert.ToInt32(userAchievement.UserAccountId)))
                {
                    return BadRequest($"User with ID {userAchievement.UserAccountId} not found");
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

        public async Task<IActionResult> GetByUsernameAsync([FromQuery] string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return BadRequest("Username cannot be empty.");
                }

                if (!await _achievementsService.ValidateUsernameAsync(username))
                {
                    return NotFound($"User '{username}' not found.");
                }

                var achievement = await _achievementsService.GetAchievementByUsurnameAsync(username);
                if (achievement == null)
                {
                    return NotFound($"No achievements found for user '{username}'.");
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
