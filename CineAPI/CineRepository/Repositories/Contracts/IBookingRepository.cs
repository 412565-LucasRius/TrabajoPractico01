using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IBookingRepository
    {
    Task<IEnumerable<Booking>> GetBookingsByUser(string userId);

    Task<Booking> GetBookingById(int id);

    Task<bool> SaveBooking(Booking booking);

    Task<Booking> UpdateBookingState(Booking booking);
    }
  }
