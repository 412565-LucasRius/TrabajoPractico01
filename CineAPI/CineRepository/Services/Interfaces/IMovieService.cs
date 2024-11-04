using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Services.Interfaces
{
    public interface IMovieService
    {

        Task<IEnumerable<Movie>> GetAllPremiereAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<Movie>> GetMovieByGenreAsync(int genre);
        Task<IEnumerable<Movie>> GetMoviesByScreenTypeAsync(int screenTypeId);


    }
}
