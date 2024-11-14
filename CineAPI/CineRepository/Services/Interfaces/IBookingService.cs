﻿using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using System.Threading.Tasks;

namespace CineRepository.Services.Interfaces
  {
  public interface IBookingService
    {

    Task<IEnumerable<Booking>> GetBookingsByUser(int userId);

    Task<List<string>> GetBookedSeatNumbersByShowtimeId(int showtimeId);

    Task<List<string>> GetBookedSeatNumbersByShowtimeIdandBookingId(int showtimeId, int bookingid);

    Task<bool> UpdateBooking(int bookingId, List<TicketRequest> ticketsList);


    Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId);

    Task<Booking> GetBookingById(int id);

    Task<bool> SaveBooking(BookingRequest bookingRequest, List<TicketRequest> ticketRequest);

    Task<bool> UpdateBookingState(int id, int state);
    }
  }
