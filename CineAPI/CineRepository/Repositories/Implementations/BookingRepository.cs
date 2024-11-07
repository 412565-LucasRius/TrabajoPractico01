using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Booking>> GetBookingsByUser(int userId)
      {
      var bookingData = await _context.Bookings
          .Where(b => b.Customer.CustomerId == userId)
          .Include(b => b.Tickets)
              .ThenInclude(t => t.Showtime)
                  .ThenInclude(s => s.Movie)
          .Include(b => b.Tickets)
              .ThenInclude(t => t.Showtime)
                  .ThenInclude(s => s.Screen)
                      .ThenInclude(sc => sc.Cinema)
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
          var ticketEntity = new Ticket
            {
            ShowtimeId = ticket.ShowtimeId,
            BookingId = booking.BookingId,
            SeatNumber = ticket.SeatNumber,
            SaleDate = ticket.SaleDate,
            Price = ticket.Price,
            };

          _context.Tickets.Add(ticketEntity);
          }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return true;
        }
      catch (Exception)
        {
        await transaction.RollbackAsync();
        return false;
        }
      }

    public async Task<bool> UpdateBookingState(int id, int state)
      {
      var booking = await _context.Bookings.FirstOrDefaultAsync(i => i.BookingId == id);

      if (booking != null)
        {
        booking.BookingStateId = state;
        _context.Bookings.Update(booking);

        }
      return await _context.SaveChangesAsync() > 0;

      }
    }
  }
