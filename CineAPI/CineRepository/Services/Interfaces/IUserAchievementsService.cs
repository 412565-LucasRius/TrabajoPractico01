using CineRepository.Models.DTO;
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
        Task<IEnumerable<object>> GetAchievementByUserIdAsync(int userId);
        Task<bool> UserIdExistsAsync(int userId);

        Task<IEnumerable<object>> GetAchievementsByIdsAsync(List<int> achievementIds);
        Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement);
        Task<bool> AchievementExistsAsync(int achievementId);

    }
}
