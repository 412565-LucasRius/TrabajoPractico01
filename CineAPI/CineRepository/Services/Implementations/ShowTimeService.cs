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

        public Task<IEnumerable<Showtime>> GetAllShowTimesAsync()
        {
            return _showTimeRepository.GetAllShowTimesAsync();
        }

        public Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync()
        {
            return _showTimeRepository.GetAllShowTimesAvaiblesAsync();
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndDateAsync(int cinemaId, DateTime date)
        {
            return _showTimeRepository.GetShowTimesByCinemaAndDateAsync(cinemaId, date);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndMovieAsync(int cinemaId, int movieId)
        {
            return _showTimeRepository.GetShowTimesByCinemaAndMovieAsync(cinemaId, movieId);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId)
        {
            return _showTimeRepository.GetShowTimesByCinemaIdAsync(cinemaId);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByCinemaMovieAndDateAsync(int cinemaId, int movieId, DateTime date)
        {
            return _showTimeRepository.GetShowTimesByCinemaMovieAndDateAsync(cinemaId, movieId, date);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByDateAsync(DateTime date)
        {
            return _showTimeRepository.GetShowTimesByDateAsync(date);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByMovieAndDateAsync(int movieId, DateTime date)
        {
            return _showTimeRepository.GetShowTimesByMovieAndDateAsync(movieId, date);
        }

        public Task<IEnumerable<Showtime>> GetShowTimesByMovieIdAsync(int movieId)
        {
            return _showTimeRepository.GetShowTimesByMovieIdAsync(movieId);
        }
    }
}
