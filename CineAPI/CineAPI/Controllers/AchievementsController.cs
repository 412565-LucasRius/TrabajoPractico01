using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
  {
  [Route("api/[controller]")]
  [ApiController]
  public class AchievementsController : ControllerBase
    {

    public readonly IAchievementsService _achievementsService;

    public AchievementsController(IAchievementsService bookingService)
      {
      _achievementsService = bookingService;
      }

    [HttpGet]
    public async Task<IActionResult> GetAchievements()
      {
      try
        {
        var achievements = await _achievementsService.GetAchievementsAsync();
        return Ok(achievements);
        }
      catch (Exception ex)
        {
        return StatusCode(500, "An internal error occurred: " + ex.Message);
        }
      }
    }
  }
