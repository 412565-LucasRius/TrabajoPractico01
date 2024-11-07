using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

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
                          select movie).Distinct().ToListAsync();
        }



    public async Task<MovieDTO> GetMovieByIdAsync(int id)
      {
      var movie = await _context.Movies
          .Include(m => m.Genre)
          .Include(m => m.Producer)
          .Include(m => m.MovieDirectors)
              .ThenInclude(md => md.Director)
          .Include(m => m.Showtimes)
          .FirstOrDefaultAsync(m => m.MovieId == id);

      if (movie == null)
        return null;

      return new MovieDTO
        {
        MovieId = movie.MovieId,
        Title = movie.Title,
        ReleaseDate = movie.ReleaseDate,
        Genre = movie.Genre,
        Producer = movie.Producer,
        Duration = movie.Duration,
        Showtimes = movie.Showtimes.Select(s => new ShowtimeDTO
          {
          ShowtimeId = s.ShowtimeId,
          StartDate = s.StartDate,
          EndDate = s.EndDate,
          ScreenId = s.ScreenId ?? 0
          }).ToList()
        };
      }


    }

  }
