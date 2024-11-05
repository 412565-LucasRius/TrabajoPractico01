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
        
        Task<IEnumerable<Showtime>> GetAllShowTimesAvaiblesAsync(); // Trae todos las funciones disponibles.
        Task<IEnumerable<Showtime>> GetShowTimesByCinemaIdAsync(int cinemaId); // Trae todas las funciones de un cine.
 


    }
}
