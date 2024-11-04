using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Services.Interfaces
{
    public interface IUserAchievementsService
    {
        Task<IEnumerable<UserAchievement>> GetAchievementByUsurnameAsync(string username);
        Task<bool> ValidateUsernameAsync(string username);

        //create
        Task<UserAchievement> CreateAchievementAsync(UserAchievement userAchievement);
        Task<bool> UserExistsAsync(int userAccountId);
        Task<bool> AchievementExistsAsync(int achievementId);

    }
}
