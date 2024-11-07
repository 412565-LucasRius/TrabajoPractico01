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
    public class UserGenreStatService : IUserGenreStatService
    {
        private readonly IUserGenreStatRepository _userGenreStatRepository;

        public UserGenreStatService(IUserGenreStatRepository userGenreStatRepository)
        {
            _userGenreStatRepository = userGenreStatRepository;
        }

        public async Task<IEnumerable<UserGenreStat>> GetUserGenreStatsWithUserAccountAndGenreAsync(int userAccountId)
        {
            return await _userGenreStatRepository.GetUserGenreStatsWithUserAccountAndGenreAsync(userAccountId);
        }
    }
}
