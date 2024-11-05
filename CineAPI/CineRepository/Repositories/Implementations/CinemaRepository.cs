using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Implementations
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly Cine_1W3_TPContext _context;

        public CinemaRepository(Cine_1W3_TPContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
        {
            return await _context.Cinemas.ToListAsync();

        }
    }
}
