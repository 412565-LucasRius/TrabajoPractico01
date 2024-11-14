using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CineRepository.Repositories.Implementations
  {
  public class BookingRepository : IBookingRepository
    {

    private readonly Cine_1W3_TPContext _context;

    public BookingRepository(Cine_1W3_TPContext context)
      {
      _context = context;
      }
    public async Task<Booking> GetBookingById(int id)
      {
      return await _context.Bookings.FirstOrDefaultAsync(i => i.BookingId == id);
      }
    public async Task<List<string>> GetBookedSeatNumbersByShowtimeId(int showtimeId)
    {
        try
        {
            var bookedSeats = await _context.Tickets
                .Where(t => t.ShowtimeId == showtimeId &&
                            t.Booking.BookingStateId != 3) // Excluye reservas canceladas
                .Select(t => t.SeatNumber)
                .Distinct()
                .ToListAsync();

            return bookedSeats;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener los asientos reservados: {ex.Message}");
        }
    }
    public async Task<List<string>> GetBookedSeatNumbersByShowtimeIdandBookingId(int showtimeId, int bookingid)
    {
        try
        {
            var bookedSeats = await _context.Tickets
                .Where(t => t.ShowtimeId == showtimeId &&
                            t.Booking.BookingStateId != 3 &&
                            t.BookingId != bookingid
                            ) // Excluye reservas canceladas
                .Select(t => t.SeatNumber)
                .Distinct()
                .ToListAsync();

            return bookedSeats;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener los asientos reservados: {ex.Message}");
        }
    }
        public async Task<bool> UpdateBooking(int bookingId, List<TicketRequest> ticketsList)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Obtener la reserva de manera asíncrona
                var booking = await GetBookingById(bookingId);
                if (booking == null)
                    return false;

                booking.BookingDate = ticketsList[0].SaleDate;
                _context.Bookings.Update(booking);

                await _context.SaveChangesAsync();

                // Obtiene los tickets actuales y los elimina
                var existingTickets = await _context.Tickets.Where(t => t.BookingId == bookingId).ToListAsync();
                _context.Tickets.RemoveRange(existingTickets);
                await _context.SaveChangesAsync();  // Guarda cambios tras eliminación

                // Agrega los nuevos tickets
                foreach (TicketRequest ticket in ticketsList)
                {
                    var ticketEntity = new Ticket
                    {
                        ShowtimeId = ticket.ShowtimeId,
                        BookingId = bookingId,
                        SeatNumber = ticket.SeatNumber,
                        SaleDate = ticket.SaleDate,
                        Price = 1500
                    };
                    _context.Tickets.Add(ticketEntity);
                }

                await _context.SaveChangesAsync();  // Guarda cambios tras la adición
                await transaction.CommitAsync();    // Confirma la transacción
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();  // Revierte en caso de error
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUser(int userId)
    {
    var bookingData = await _context.Bookings
        .Where(b => b.Customer.CustomerId == userId &&
                            b.BookingStateId != 3)
        .Include(b => b.Tickets)
            .ThenInclude(t => t.Showtime)
                .ThenInclude(s => s.Movie)
        .Include(b => b.Tickets)
            .ThenInclude(t => t.Showtime)
                .ThenInclude(s => s.Screen)
                    .ThenInclude(sc => sc.Cinema)
        .Include(b => b.BookingState)
        .OrderByDescending(b => b.BookingDate) // Orden descendente por BookingState
        .ThenBy(b => b.BookingState)
        .AsNoTracking()
        .ToListAsync();
    return bookingData;
    }

    public async Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId)
      {
      try
        {

        var bookings = await _context.Bookings
            .FromSqlRaw("EXEC GetBookingByUserAccountId @UserAccountId = {0}", userAccountId)
            .Include(b => b.Tickets)
            .ToListAsync();

        return bookings;
        }
      catch (Exception ex)
        {

        throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
        }
      }

        public async Task<bool> SaveBooking(BookingRequest bookingRequest, List<TicketRequest> ticketRequest)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Crear la reserva de la compra
                var booking = new Booking
                {
                    CustomerId = bookingRequest.CustomerId,
                    BookingDate = bookingRequest.BookingDate,
                    BookingStateId = 1
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                foreach (var ticket in ticketRequest)
                {
                    // Obtener el screen_id asociado al ShowtimeId del ticket
                    var showtime = await _context.Showtimes
                        .Where(s => s.ShowtimeId == ticket.ShowtimeId)
                        .FirstOrDefaultAsync();

                    if (showtime == null)
                    {
                        // Si no se encuentra el Showtime, puedes lanzar un error o hacer alguna validación
                        throw new Exception("Showtime no encontrado.");
                    }

                    // Obtener la capacidad de la sala desde la tabla Screens
                    var screen = await _context.Screens
                        .Where(s => s.ScreenId == showtime.ScreenId)
                        .FirstOrDefaultAsync();

                    if (screen == null)
                    {
                        // Si no se encuentra la sala, puedes lanzar un error
                        throw new Exception("Sala no encontrada.");
                    }

                    // Obtener la cantidad de tickets vendidos, incluyendo el ticket que se está intentando agregar
                    var totalTickets = await _context.Tickets
                        .Where(t => t.ShowtimeId == ticket.ShowtimeId)
                        .CountAsync();

                    // Verificar si la capacidad de la sala se ha superado
                    if (1 + screen.SeatsTaken > screen.Capacity)
                    {
                        throw new Exception("La capacidad de la sala ha sido superada.");
                    }

                    // Crear el ticket
                    var ticketEntity = new Ticket
                    {
                        ShowtimeId = ticket.ShowtimeId,
                        BookingId = booking.BookingId,
                        SeatNumber = ticket.SeatNumber,
                        SaleDate = ticket.SaleDate,
                        Price = 1500
                    };

                    _context.Tickets.Add(ticketEntity);

                    // Actualizar el número de asientos ocupados en la sala (SeatsTaken)
                    screen.SeatsTaken++;
                    _context.Screens.Update(screen);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    public async Task<bool> UpdateBookingState(int id, int state)
    {
       var booking = await _context.Bookings
            .Include(b => b.Tickets)
                .ThenInclude(t => t.Showtime)
                    .ThenInclude(s => s.Screen)
            .FirstOrDefaultAsync(b => b.BookingId == id);

        if (booking != null)
        {
          var ticketsByScreen = booking.Tickets.GroupBy(t => t.Showtime.Screen);

            foreach (var screenGroup in ticketsByScreen)
            {
                var screen = screenGroup.Key;
                var ticketCount = screenGroup.Count();

                // Restamos la cantidad de tickets del seats_taken
                screen.SeatsTaken -= ticketCount;

                // Aseguramos que seats_taken no sea negativo
                if (screen.SeatsTaken < 0)
                {
                    screen.SeatsTaken = 0;
                }

                _context.Screens.Update(screen);
            }
             booking.BookingStateId = state;
            _context.Bookings.Update(booking);

        }
        return await _context.SaveChangesAsync() > 0;

    }
    }
  }
