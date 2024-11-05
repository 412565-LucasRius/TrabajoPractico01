using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Services.Implementations
{
    public class ShowTimeService: IShowTimeService
    {
        private readonly IShowTimeRepository _showTimeRepository;

        public ShowTimeService(IShowTimeRepository showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }



        public Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync()
        {
            return _showTimeRepository.GetAllShowTimesAvaiblesAsync();
        }

        



        public Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId)
        {
            return _showTimeRepository.GetShowTimesByCinemaIdAsync(cinemaId);
        }


    }
}
