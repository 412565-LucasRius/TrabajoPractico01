using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Services.Implementations;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("GetBookingById")]
        public async Task<IActionResult> GetbyIdGetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingById(id);

                if (booking == null)
                {
                    return BadRequest("Booking no existe");
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
            }
        }
        [HttpGet("GetBookingByUser")]
        public async Task<IActionResult> GetbyIdGetBookingByUser(int userId)
        {
            try
            {
                var booking = await _bookingService.GetBookingById(userId);

                if (booking == null)
                {
                    return BadRequest("Booking no existe");
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
            }
        }

        [HttpGet("GetBookingByUserwithSP")]
        public async Task<IActionResult> GetbyIdGetBookingByUserWihSP(int userId)
        {
            try
            {
                var booking = await _bookingService.GetBookingsByUserAccountIdAsync(userId);

                if (booking == null)
                {
                    return BadRequest("Booking no existe");
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
            }
        }

        [HttpPost]

        public async Task<IActionResult> SaveBooking([FromBody] Booking booking)
        {
            try
            {
                var obooking = await _bookingService.SaveBooking(booking);

                if (obooking == null)
                {
                    return BadRequest("Booking no existe");
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
            }
        }

        [HttpPut("{id}/{state}")]
        public async Task<IActionResult> UpdateBookingState(int id, int state)
        {
            try
            {
                bool updated = await _bookingService.UpdateBookingState(id, state);

                if (updated)
                {
                    return Ok(new { message = "Componente actualizado con éxito", id, state });
                }
                else
                {
                    return NotFound(new { message = "No se encontró la reserva o el estado no cambió.", id });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
            }
        }

    }


}
