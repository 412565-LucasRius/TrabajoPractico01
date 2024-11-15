using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IMoviesRepository
    {
    Task<IEnumerable<Movie>> GetAllPremiereAsync(); 
    Task<MovieDTO> GetMovieByIdAsync(int id);



    }
  }
