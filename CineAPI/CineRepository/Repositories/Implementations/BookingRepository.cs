using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;

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
                            t.Booking.BookingStateId != 3) 
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
                            ) 
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
                
                var booking = await GetBookingById(bookingId);
                if (booking == null)
                    return false;

                booking.BookingDate = ticketsList[0].SaleDate;
                _context.Bookings.Update(booking);

                await _context.SaveChangesAsync();

                
                var existingTickets = await _context.Tickets.Where(t => t.BookingId == bookingId).ToListAsync();
                _context.Tickets.RemoveRange(existingTickets);
                await _context.SaveChangesAsync();  

                
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
        .OrderByDescending(b => b.BookingDate) 
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
                    
                    var showtime = await _context.Showtimes
                        .Where(s => s.ShowtimeId == ticket.ShowtimeId)
                        .FirstOrDefaultAsync();

                    if (showtime == null)
                    {
                        
                        throw new Exception("Showtime no encontrado.");
                    }

                    
                    var screen = await _context.Screens
                        .Where(s => s.ScreenId == showtime.ScreenId)
                        .FirstOrDefaultAsync();

                    if (screen == null)
                    {
                        
                        throw new Exception("Sala no encontrada.");
                    }

                    
                    var totalTickets = await _context.Tickets
                        .Where(t => t.ShowtimeId == ticket.ShowtimeId)
                        .CountAsync();

                    
                    if (1 + screen.SeatsTaken > screen.Capacity)
                    {
                        throw new Exception("La capacidad de la sala ha sido superada.");
                    }

                    
                    var ticketEntity = new Ticket
                    {
                        ShowtimeId = ticket.ShowtimeId,
                        BookingId = booking.BookingId,
                        SeatNumber = ticket.SeatNumber,
                        SaleDate = ticket.SaleDate,
                        Price = 1500
                    };

                    _context.Tickets.Add(ticketEntity);

                    
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

                
                screen.SeatsTaken -= ticketCount;

                
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
    public async Task<string> GetUniqueBookingCountsByGenreAsync(int userId, int genreId)
    {
            var lista = new List<int>();

            
            var totalBookingCount = await _context.Bookings
                .Where(b => b.CustomerId == userId && b.BookingStateId == 1)
                .CountAsync();

            
            var uniqueBookingsCount = await (
                from b in _context.Bookings
                join t in _context.Tickets on b.BookingId equals t.BookingId
                join s in _context.Showtimes on t.ShowtimeId equals s.ShowtimeId
                join m in _context.Movies on s.MovieId equals m.MovieId
                where b.BookingStateId == 1 && m.GenreId == genreId && b.CustomerId == userId
                select b.BookingId
            ).Distinct().CountAsync();

            switch (totalBookingCount)
            {
                case > 4:
                    lista.AddRange(new[] { 1, 2, 3 });
                    break;
                case > 2:
                    lista.AddRange(new[] { 1, 2 });
                    break;
                case > 0:
                    lista.Add(1);
                    break;
            }
            switch (uniqueBookingsCount)
            {
                case > 2:
                    lista.AddRange(new[] { 4, 5 });
                    break;
                case > 1:
                    lista.AddRange(new[] { 4 });
                    break;
            }

            return JsonSerializer.Serialize(lista);
        }


    }
}
