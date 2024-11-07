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
            return await _context.Bookings
          .Where(booking => booking.CustomerId == userId)
          .ToListAsync(); ;
      }

    public async Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId)
    {
        try
        {
            
            var bookings = await _context.Bookings
                .FromSqlRaw("EXEC GetBookingByUserAccountId @UserAccountId = {0}", userAccountId)
                .ToListAsync();

            return bookings;
        }
        catch (Exception ex)
        {
            
            throw new Exception("Error al ejecutar el procedimiento almacenado.", ex);
        }
    }

        public async Task<bool> SaveBooking(Booking booking)
      {
        _context.Bookings.Add(booking);
        return await _context.SaveChangesAsync() > 0;
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
