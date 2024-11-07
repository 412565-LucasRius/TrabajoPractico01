using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IMovieService
    {

    Task<IEnumerable<Movie>> GetAllPremiereAsync();
    Task<MovieDTO> GetMovieByIdAsync(int id);


    }
  }
