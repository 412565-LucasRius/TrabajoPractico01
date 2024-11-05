using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Services.Interfaces
{
    public interface IBookingService
    {
     
    Task<IEnumerable<Booking>> GetBookingsByUser(int userId);

    Task<List<Booking>> GetBookingsByUserAccountIdAsync(int userAccountId);

    Task<Booking> GetBookingById(int id);

    Task<bool> SaveBooking(Booking booking);

    Task<bool> UpdateBookingState(int id, int state);
    }
}
