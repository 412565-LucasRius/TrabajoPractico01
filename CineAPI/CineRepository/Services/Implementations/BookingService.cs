using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Repositories.Implementations;
using CineRepository.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<bool> SaveBooking(Booking booking)
        {
            return await _bookingRepository.SaveBooking(booking);
        }

        public async Task<bool> UpdateBookingState(int id, int state)
        {
           return await _bookingRepository.UpdateBookingState( id, state);
        }

        public async Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId)
        {
            return await _bookingRepository.GetBookingsByUserAccountIdAsync(userAccountId);
        }
    }
}
