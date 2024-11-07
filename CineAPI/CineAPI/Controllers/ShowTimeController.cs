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


    }
}
