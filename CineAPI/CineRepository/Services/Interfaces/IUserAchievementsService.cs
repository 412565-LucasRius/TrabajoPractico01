using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IUserAchievementsService
    {
    Task<IEnumerable<UserAchievement>> GetAchievementByUsernameAsync(int userId);
    Task<bool> ValidateUsernameAsync(int userId);

    //create
    Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement);
    Task<bool> UserExistsAsync(int userAccountId);
    Task<bool> AchievementExistsAsync(int achievementId);

    }
  }
