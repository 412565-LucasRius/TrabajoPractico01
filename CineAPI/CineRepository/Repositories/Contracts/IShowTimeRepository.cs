using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface IShowTimeRepository
    {
        Task<IEnumerable<Showtime>> GetAllShowTimesAsync(); // Trae todos las funciones.
        Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync(); // Trae todos las funciones disponibles.
        Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId); // Trae todas las funciones de un cine.
        Task<IEnumerable<Showtime>> GetShowTimesByMovieIdAsync(int movieId); // Trae todas las funciones de una película.
        Task<IEnumerable<Showtime>> GetShowTimesByDateAsync(DateTime date); // Trae todas las funciones de una fecha.
        Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndDateAsync(int cinemaId, DateTime date); // Trae todas las funciones de un cine y una fecha.
        Task<IEnumerable<Showtime>> GetShowTimesByMovieAndDateAsync(int movieId, DateTime date); // Trae todas las funciones de una película y una fecha.
        Task<IEnumerable<Showtime>> GetShowTimesByCinemaAndMovieAsync(int cinemaId, int movieId); // Trae todas las funciones de un cine y una película.
        Task<IEnumerable<Showtime>> GetShowTimesByCinemaMovieAndDateAsync(int cinemaId, int movieId, DateTime date); // Trae todas las funciones de un cine, una película y una fecha.

    }
}
