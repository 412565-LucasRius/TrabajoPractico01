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
    public class UserGenreStatRepository : IUserGenreStatRepository
    {
        private readonly Cine_1W3_TPContext _context;

        public UserGenreStatRepository(Cine_1W3_TPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserGenreStat>> GetUserGenreStatsWithUserAccountAndGenreAsync(int userAccountId)
        {
            return await _context.UserGenreStats
                .Include(ugs => ugs.UserAccount)
                .Include(ugs => ugs.Genre)
                .Where(ugs => ugs.UserAccountId == userAccountId)
                .ToListAsync();
        }
    }
}
