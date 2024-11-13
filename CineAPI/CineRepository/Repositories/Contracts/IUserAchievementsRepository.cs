using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IUserAchievementsRepository
    {
    Task<IEnumerable<object>> GetAchievementByUserIdAsync(int userId);
    Task<bool> UserIdExistsAsync(int userId);

    //create
    Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement);
    Task<bool> AchievementExistsAsync(int achievementId);

    }
  }
