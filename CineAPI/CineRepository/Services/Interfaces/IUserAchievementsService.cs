using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

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
