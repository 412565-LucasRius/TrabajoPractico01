using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Implementations
{
    public class UserAchievementsRepository : IUserAchievementsRepository
    {

        private readonly Cine_1W3_TPContext _context;

        public UserAchievementsRepository(Cine_1W3_TPContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<UserAchievement>> GetAchievementByUsurnameAsync(string username)
        {
            return await _context.UserAchievements
             .Include(ua => ua.UserAccount)
             .Include(ua => ua.Achievement)
             .Where(ua => ua.UserAccount.Username.ToLower() == username.ToLower())
             .Select(ua => new UserAchievement
             {
            UserAchievementId = ua.UserAchievementId,
            UserAccountId = ua.UserAccountId,
            AchievementId = ua.AchievementId,
            AchievedAt = ua.AchievedAt,
            Achievement = new Achievement
                 {
                    AchievementId = ua.Achievement.AchievementId,
                    Name = ua.Achievement.Name,
                    Description = ua.Achievement.Description
                 }
             }      )
        .ToListAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.UserAccounts
                .AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        //create

        public async Task<UserAchievement> CreateAchievementAsync(UserAchievement userAchievement)
        {
            await _context.UserAchievements.AddAsync(userAchievement);
            await _context.SaveChangesAsync();
            return userAchievement;
        }

        public async Task<bool> UserExistsAsync(int userAccountId)
        {
            return await _context.UserAccounts.AnyAsync(u => u.UserAccountId == userAccountId);
        }

        public async Task<bool> AchievementExistsAsync(int achievementId)
        {
            return await _context.Achievements.AnyAsync(a => a.AchievementId == achievementId);
        }

    }
}
