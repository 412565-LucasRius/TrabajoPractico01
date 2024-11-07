using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IBookingService
    {

    Task<IEnumerable<Booking>> GetBookingsByUser(int userId);

    Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId);

    Task<Booking> GetBookingById(int id);

    Task<bool> SaveBooking(BookingRequest bookingRequest, List<TicketRequest> ticketRequest);

    Task<bool> UpdateBookingState(int id, int state);
    }
  }
