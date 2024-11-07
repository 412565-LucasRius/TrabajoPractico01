using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface IUserGenreStatRepository
    {
        Task<IEnumerable<UserGenreStat>> GetUserGenreStatsWithUserAccountAndGenreAsync(int userAccountId);
    }
}
