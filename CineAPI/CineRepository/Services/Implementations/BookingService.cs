using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

     public async Task<List<string>> GetBookedSeatNumbersByShowtimeIdandBookingId(int showtimeId, int bookingid)
        {
            return await _bookingRepository.GetBookedSeatNumbersByShowtimeIdandBookingId(showtimeId, bookingid);
        }
    public async Task<List<string>> GetBookedSeatNumbersByShowtimeId(int showtimeId)
    {
        return await _bookingRepository.GetBookedSeatNumbersByShowtimeId(showtimeId);
    }

    public async Task<bool> UpdateBooking(int bookingId, List<TicketRequest> ticketsList)
        {
            if (bookingId == 0 && ticketsList.Count == 0) return false;
            return await _bookingRepository.UpdateBooking(bookingId, ticketsList);
        }


    }
  }
