using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAllPremiereAsync(); // Trae todas las peliculas en Estreno.
        Task <Movie> GetMovieByIdAsync(int id); // Trae peliculas segun ID.
        Task<IEnumerable<Movie>> GetMovieByGenreAsync(int genre); //Trae pelicualas segun el genero.
        Task<IEnumerable<Movie>> GetMoviesByScreenTypeAsync(int screenTypeId); //Trae peliculas segun sea 2D,3D,4D. 
      

        
    }
}
