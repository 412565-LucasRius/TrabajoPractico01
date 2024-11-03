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

    public async Task<UserAchievement> GetAchievementByUsurnameAsync(string usurname)
      {
      var newAchievement = await _userAchievementsRepository.GetAchievementByUsurnameAsync(usurname);

      if (newAchievement != null)
        return newAchievement;

      return null;
      }

    public async Task<UserAchievement> PostAchievementAsync(UserAchievement newUserAchievement)
      {
      if (newUserAchievement.AchievementId == null || newUserAchievement.AchievementId == 0)
        return null;
      if (newUserAchievement.UserAccountId == null || newUserAchievement.UserAccountId == 0)
        return null;

      var result = await _userAchievementsRepository.PostAchievementAsync(newUserAchievement);
      return result;
      }
    }
  }
