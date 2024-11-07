using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;

namespace CineRepository.Services.Implementations
  {
  public class MovieService : IMovieService
    {
    private readonly IMoviesRepository _movieRepository;

    public MovieService(IMoviesRepository movieRepository)
      {
      _movieRepository = movieRepository;
      }
    public async Task<IEnumerable<Movie>> GetAllPremiereAsync()
      {
      return await _movieRepository.GetAllPremiereAsync();
      }

    public async Task<MovieDTO> GetMovieByIdAsync(int id)
      {
      return await _movieRepository.GetMovieByIdAsync(id);
      }


    }
  }
