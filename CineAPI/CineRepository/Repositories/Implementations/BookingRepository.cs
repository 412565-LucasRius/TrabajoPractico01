using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;

namespace CineRepository.Repositories.Implementations
  {
  public class BookingRepository : IBookingRepository
    {
    public Task<Booking> GetBookingById(int id)
      {
      throw new NotImplementedException();
      }

    public Task<IEnumerable<Booking>> GetBookingsByUser(string userId)
      {
      throw new NotImplementedException();
      }

    public Task<bool> SaveBooking(Booking booking)
      {
      throw new NotImplementedException();
      }

    public Task<Booking> UpdateBookingState(Booking booking)
      {
      throw new NotImplementedException();
      }
    }
  }
