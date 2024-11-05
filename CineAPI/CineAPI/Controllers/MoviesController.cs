using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("GetAllPremiere")]

        public async Task<IActionResult> GetAllPremiere()
        {
            try
            {
                var movies = await _movieService.GetAllPremiereAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetMovieById")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       

        
    }
}