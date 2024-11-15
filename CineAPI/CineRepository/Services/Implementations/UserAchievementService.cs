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

        public async Task<IEnumerable<object>> GetAchievementByUserIdAsync(int userId)
        {
            return await _userAchievementsRepository.GetAchievementByUserIdAsync(userId);
        }

        public async Task<bool> UserIdExistsAsync(int userId)
        {
            if (userId == 0)
                return false;

            return await _userAchievementsRepository.UserIdExistsAsync(userId);
        }

        

        public async Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement)
        {
            return await _userAchievementsRepository.CreateAchievementAsync(userAchievement);
        }

        public async Task<bool> AchievementExistsAsync(int achievementId)
        {
            return await _userAchievementsRepository.AchievementExistsAsync(achievementId);
        }

        public async Task<IEnumerable<object>> GetAchievementsByIdsAsync(List<int> achievementIds)
        {
            return await _userAchievementsRepository.GetAchievementsByIdsAsync(achievementIds);
        }
    }
}
