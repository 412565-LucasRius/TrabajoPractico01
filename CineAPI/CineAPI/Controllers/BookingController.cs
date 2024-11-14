using CineRepository.Models.DTO;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    //[Authorize]
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
    //[Authorize]
    public async Task<IActionResult> GetBookingByUser(int userId)
      {
      try
        {
        var booking = await _bookingService.GetBookingsByUser(userId);

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
    [HttpGet("BookedByShowtimeId/{showtimeId}")]
    public async Task<ActionResult<List<string>>> GetBookedSeats(int showtimeId)
      {
      try
        {
        var bookedSeats = await _bookingService.GetBookedSeatNumbersByShowtimeId(showtimeId);

        if (bookedSeats == null || !bookedSeats.Any())
          {
          return Ok(new List<string>()); // Retorna lista vacía si no hay asientos reservados
          }

        return Ok(bookedSeats);
        }
      catch (Exception ex)
        {
        return StatusCode(500, new { message = "Error al obtener los asientos reservados", error = ex.Message });
        }

    }
    [HttpGet("BookedByShowtimeIdandBooking/{showtimeId}/{bookingid}")]
    public async Task<ActionResult<List<string>>> GetBookedSeatsbyShowandBook(int showtimeId, int bookingid)
    {
        try

        {
            var bookedSeats = await _bookingService.GetBookedSeatNumbersByShowtimeIdandBookingId(showtimeId, bookingid);

            if (bookedSeats == null || !bookedSeats.Any())
            {
                return Ok(new List<string>()); // Retorna lista vacía si no hay asientos reservados
            }

            return Ok(bookedSeats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los asientos reservados", error = ex.Message });
        }
    }
     [HttpPost]
    //[Authorize]
    public async Task<IActionResult> SaveBooking([FromBody] BookingWithTicketsRequest bookingWithTicketsRequest)
      {
      try
        {
        var isSuccess = await _bookingService.SaveBooking(bookingWithTicketsRequest.BookingRequest, bookingWithTicketsRequest.TicketRequest);

        if (!isSuccess)
          {
          return BadRequest("No se puedo crear la reserva o los tickets");
          }

        return Ok(new
          {
          message = "Reserva y tickets creados correctamente",
          booking = bookingWithTicketsRequest.BookingRequest,
          tickets = bookingWithTicketsRequest.TicketRequest,
          });
        }
      catch (Exception ex)
        {
        return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
        }
      }
    [HttpPut("UpdateBooking")]
    //[Authorize]
    public async Task<IActionResult> UpdateBooking([FromQuery] int bookingId, [FromBody] List<TicketRequest> ticketList)
    {
        try
        {
            var isSuccess = await _bookingService.UpdateBooking(bookingId, ticketList);

            if (!isSuccess)
            {
                    return BadRequest("No se puedo actualizar la reserva.");
            }

            return Ok(new
            {
                message = "Reserva actualizada correctamente",

            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ha ocurrido un error interno.", error = ex.Message });
        }
    }

        [HttpPut("DeleteBooking/{id}/{state}")]
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
