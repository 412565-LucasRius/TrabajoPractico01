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

        public async Task<IEnumerable<Movie>> GetMovieByGenreAsync(int genre)
        {
            return await _movieRepository.GetMovieByGenreAsync(genre);
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }
    }
}
