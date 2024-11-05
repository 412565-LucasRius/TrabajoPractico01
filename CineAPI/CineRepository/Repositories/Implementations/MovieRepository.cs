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

            return await _context.Movies
                .Where(m => _context.Showtimes
                    .Any(s => s.MovieId == m.MovieId && s.EndDate < currentDate))
                .ToListAsync();
        }



        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }


    }

}
