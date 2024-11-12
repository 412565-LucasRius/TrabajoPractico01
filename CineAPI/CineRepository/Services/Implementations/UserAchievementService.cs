using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;


namespace CineRepository.Services.Implementations
  {
  public class UserAchievementService : IUserAchievementsService
    {
    private readonly IUserAchievementsRepository _userAchievementsRepository;

    public UserAchievementService(IUserAchievementsRepository userAchievementsRepository)
      {
      _userAchievementsRepository = userAchievementsRepository;
      }

    public async Task<IEnumerable<UserAchievement>> GetAchievementByUsernameAsync(int userId)
      {
      return await _userAchievementsRepository.GetAchievementByUsernameAsync(userId);
      }

    public async Task<bool> ValidateUsernameAsync(int userId)
      {
      if (userId == 0)
        return false;

      return await _userAchievementsRepository.UserExistsAsync(userId);
      }

    //create

    public async Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement)
      {
      return await _userAchievementsRepository.CreateAchievementAsync(userAchievement);
      }

    public async Task<bool> UserExistsAsync(int userAccountId)
      {
      return await _userAchievementsRepository.UserExistsAsync(userAccountId);
      }

    public async Task<bool> AchievementExistsAsync(int achievementId)
      {
      return await _userAchievementsRepository.AchievementExistsAsync(achievementId);
      }
    }
  }
