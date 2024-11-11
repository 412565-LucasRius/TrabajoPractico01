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

            var movies = await (from movie in _context.Movies
                                join showtime in _context.Showtimes on movie.MovieId equals showtime.MovieId
                                join genre in _context.Genres on movie.GenreId equals genre.GenreId
                                join clasificacion in _context.Clasifications on movie.ClasificationId equals clasificacion.ClasificationId
                                join producer in _context.Producers on movie.ProducerId equals producer.ProducerId
                                where showtime.EndDate >= currentDate && showtime.StartDate <= currentDate
                                select new
                                {
                                    movie.MovieId,
                                    movie.Title,
                                    movie.ReleaseDate,
                                    movie.LastReleaseDate,
                                    Genre = genre,
                                    Clasification = clasificacion,
                                    Producer = producer,
                                    movie.Duration,
                                    movie.ImageName,
                                    Showtimes = movie.Showtimes
                                }).ToListAsync();

            var distinctMovies = movies.GroupBy(m => m.MovieId).Select(g => g.First()).ToList();

            return distinctMovies.Select(m => new Movie
            {
                MovieId = m.MovieId,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                LastReleaseDate = m.LastReleaseDate,
                Genre = m.Genre,
                Clasification = m.Clasification,
                Producer = m.Producer,
                Duration = m.Duration,
                ImageName = m.ImageName,
                Showtimes = m.Showtimes
            });
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
