using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimeController : ControllerBase
    {

        private readonly IShowTimeService _showTimeService;

        public ShowTimeController(IShowTimeService showTimeService)
        {
            _showTimeService = showTimeService;
        }

        [HttpGet("GetAllShowTimes")]
        public async Task<IActionResult> GetAllShowTimes()
        {
            try
            {
                var showTimes = await _showTimeService.GetAllShowTimesAsync();
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllShowTimesAvaibles")]

        public async Task<IActionResult> GetAllShowTimesAvaibles()
        {
            try
            {
                var showTimes = await _showTimeService.GetAllShowTimesAvaiblesAsync();
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByCinemaAndDate")]

        public async Task<IActionResult> GetShowTimesByCinemaAndDate(int cinemaId, DateTime date)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByCinemaAndDateAsync(cinemaId, date);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByCinemaAndMovie")]

        public async Task<IActionResult> GetShowTimesByCinemaAndMovie(int cinemaId, int movieId)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByCinemaAndMovieAsync(cinemaId, movieId);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByCinemaId")]

        public async Task<IActionResult> GetShowTimesByCinemaId(int cinemaId)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByCinemaIdAsync(cinemaId);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByCinemaMovieAndDate")]

        public async Task<IActionResult> GetShowTimesByCinemaMovieAndDate(int cinemaId, int movieId, DateTime date)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByCinemaMovieAndDateAsync(cinemaId, movieId, date);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByDate")]

        public async Task<IActionResult> GetShowTimesByDate(DateTime date)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByDateAsync(date);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByMovieAndDate")]

        public async Task<IActionResult> GetShowTimesByMovieAndDate(int movieId, DateTime date)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByMovieAndDateAsync(movieId, date);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetShowTimesByMovieId")]

        public async Task<IActionResult> GetShowTimesByMovieId(int movieId)
        {
            try
            {
                var showTimes = await _showTimeService.GetShowTimesByMovieIdAsync(movieId);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
