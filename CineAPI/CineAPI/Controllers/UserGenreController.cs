using CineRepository.Models.Entities;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGenreController : ControllerBase
    {
        private readonly IUserGenreStatService _userGenreStatService;

        public UserGenreController(IUserGenreStatService userGenreStatService)
        {
            _userGenreStatService = userGenreStatService;
        }

        [HttpGet("userGenresMovie")]
        public async Task<IActionResult> GetUserGenreMovies(int userAccounId)
        {
            try
            {
                var userGenreStats = await _userGenreStatService.GetUserGenreStatsWithUserAccountAndGenreAsync(userAccounId);
                if (userGenreStats == null)
                {
                    return NotFound($"No achievements found for user '{userAccounId}'.");
                }

                return Ok(userGenreStats);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An internal server error occurred." + ex.Message); ;
            }
        }

    }
}
