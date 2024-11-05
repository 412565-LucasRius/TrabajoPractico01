using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Implementations
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly Cine_1W3_TPContext _context;

        public ShowTimeRepository(Cine_1W3_TPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Showtime>> GetAllShowTimesAsync()
        {
            return await (from showtime in _context.Showtimes
                          join movie in _context.Movies on showtime.MovieId equals movie.MovieId
                          select new Showtime
                          {
                              ShowtimeId = showtime.ShowtimeId,
                              MovieId = showtime.MovieId,
                              ScreenId = showtime.ScreenId,
                              StartDate = showtime.StartDate,
                              EndDate = showtime.EndDate,
                              Movie = movie
                          }).ToListAsync();
        }



        public async Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync()
        {
            var currentDate = DateTime.Now;
            return await _context.Showtimes
                .Where(s => s.StartDate <= currentDate && s.EndDate >= currentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndDateAsync(int cinemaId, DateTime date)
        {
            return await (from showtime in _context.Showtimes
                          join screen in _context.Screens on showtime.ScreenId equals screen.ScreenId
                          where screen.CinemaId == cinemaId && showtime.StartDate <= date && showtime.EndDate >= date
                          select showtime).ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndMovieAsync(int cinemaId, int movieId)
        {
            return await (from showtime in _context.Showtimes
                          join screen in _context.Screens on showtime.ScreenId equals screen.ScreenId
                          where screen.CinemaId == cinemaId && showtime.MovieId == movieId
                          select showtime).ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId)
        {
            return await (from showtime in _context.Showtimes
                          join screen in _context.Screens on showtime.ScreenId equals screen.ScreenId
                          where screen.CinemaId == cinemaId
                          select showtime).ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByCinemaMovieAndDateAsync(int cinemaId, int movieId, DateTime date)
        {
            return await (from showtime in _context.Showtimes
                          join screen in _context.Screens on showtime.ScreenId equals screen.ScreenId
                          where screen.CinemaId == cinemaId && showtime.MovieId == movieId && showtime.StartDate <= date && showtime.EndDate >= date
                          select showtime).ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByDateAsync(DateTime date)
        {
            return await _context.Showtimes
                .Where(s => s.StartDate <= date && s.EndDate >= date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByMovieAndDateAsync(int movieId, DateTime date)
        {
            return await _context.Showtimes
                .Where(s => s.MovieId == movieId && s.StartDate <= date && s.EndDate >= date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowTimesByMovieIdAsync(int movieId)
        {
            return await _context.Showtimes
                .Where(s => s.MovieId == movieId)
                .ToListAsync();
        }
    }
}
