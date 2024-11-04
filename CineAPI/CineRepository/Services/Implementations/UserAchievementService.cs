using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Repositories.Implementations;
using CineRepository.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Services.Implementations
{
    public class UserAchievementService : IUserAchievementsService
    {
        private readonly IUserAchievementsRepository _userAchievementsRepository;

        public UserAchievementService(IUserAchievementsRepository userAchievementsRepository)
        {
            _userAchievementsRepository = userAchievementsRepository;
        }

        public async Task<IEnumerable<UserAchievement>> GetAchievementByUsurnameAsync(string username)
        {
            return await _userAchievementsRepository.GetAchievementByUsurnameAsync(username);
        }

        public async Task<bool> ValidateUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return await _userAchievementsRepository.UsernameExistsAsync(username);
        }

        //create

        public async Task<UserAchievement> CreateAchievementAsync(UserAchievement userAchievement)
        {
            userAchievement.AchievedAt = DateTime.UtcNow;
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
