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
    public class MovieRepository : IMoviesRepository
    {

        private readonly Cine_1W3_TPContext _context;

        public MovieRepository(Cine_1W3_TPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllPremiereAsync()
        {
            DateTime currentDate = DateTime.Now;

            return await (from movie in _context.Movies
                          join showtime in _context.Showtimes on movie.MovieId equals showtime.MovieId
                          where showtime.EndDate >= currentDate && showtime.StartDate <= currentDate
                          select movie).ToListAsync();
        }



        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Producer)
                .Include(m => m.MovieDirectors)
                    .ThenInclude(md => md.Director)
                .Include(m => m.Showtimes)
                    .ThenInclude(s => s.Screen)
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }


    }

}
