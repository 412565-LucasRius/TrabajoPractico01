using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface IUserAchievementsRepository
    {
        Task<IEnumerable<UserAchievement>> GetAchievementByUsurnameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);

        //create
        Task<UserAchievement> CreateAchievementAsync(UserAchievement userAchievement);
        Task<bool> UserExistsAsync(int userAccountId);
        Task<bool> AchievementExistsAsync(int achievementId);

    }
}
