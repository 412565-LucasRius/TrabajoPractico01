using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IUserAchievementsRepository
    {
    Task<IEnumerable<UserAchievement>> GetAchievementByUsernameAsync(int userId);
    Task<bool> UsernameExistsAsync(string username);

    //create
    Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement);
    Task<bool> UserExistsAsync(int userAccountId);
    Task<bool> AchievementExistsAsync(int achievementId);

    }
  }
