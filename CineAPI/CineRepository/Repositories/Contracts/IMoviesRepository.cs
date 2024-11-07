using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IMoviesRepository
    {
    Task<IEnumerable<Movie>> GetAllPremiereAsync(); // Trae todas las peliculas en Estreno.
    Task<MovieDTO> GetMovieByIdAsync(int id); // Trae peliculas segun ID.



    }
  }
