﻿using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IBookingRepository
    {
    Task<IEnumerable<Booking>> GetBookingsByUser(int userId);

    Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId);

    Task<Booking> GetBookingById(int id);

    Task<bool> SaveBooking(Booking booking);

    Task<bool> UpdateBookingState(int id, int state);
    }
  }