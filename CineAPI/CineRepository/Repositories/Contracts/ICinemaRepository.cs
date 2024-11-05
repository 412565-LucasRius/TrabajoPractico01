using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface ICinemaRepository
    {

        Task<IEnumerable<Cinema>> GetAllCinemasAsync(); // Trae todos los cines.



    }
}
