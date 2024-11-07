using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;

namespace CineRepository.Services.Implementations
  {
  public class BookingService : IBookingService
    {

    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository repository)
      {
      _bookingRepository = repository;
      }

    public async Task<Booking> GetBookingById(int id)
      {
      return await _bookingRepository.GetBookingById(id);
      }

    public async Task<IEnumerable<Booking>> GetBookingsByUser(int userId)
      {
      return await _bookingRepository.GetBookingsByUser(userId);
      }

    public async Task<bool> SaveBooking(BookingRequest bookingRequest, List<TicketRequest> ticketRequest)
      {
      return await _bookingRepository.SaveBooking(bookingRequest, ticketRequest);
      }

    public async Task<bool> UpdateBookingState(int id, int state)
      {
      return await _bookingRepository.UpdateBookingState(id, state);
      }

    public async Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId)
      {
      return await _bookingRepository.GetBookingsByUserAccountIdAsync(userAccountId);
      }
    }
  }
