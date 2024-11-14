﻿using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CineRepository.Repositories.Implementations
  {
  public class UserAchievementsRepository : IUserAchievementsRepository
    {

    private readonly Cine_1W3_TPContext _context;

    public UserAchievementsRepository(Cine_1W3_TPContext context)
      {
      _context = context;
      }

        public async Task<IEnumerable<object>> GetAchievementByUserIdAsync(int userId)
        {
            return await _context.UserAchievements
                .Where(ua => ua.UserAccount.UserAccountId == userId)
                .Select(ua => new
                {
                    ua.Achievement.Name,   // Nombre del logro
                    ua.Achievement.Points,  // Puntos asociados al logro
                    ua.Achievement.Description // descripcion de cada logro
                })
                .ToListAsync();
        }

        public async Task<bool> UserIdExistsAsync(int userId)
      {
      return await _context.UserAccounts
          .AnyAsync(u => u.UserAccountId == userId);
      }

    //create

    public async Task<UserAchievement> CreateAchievementAsync(UserAchievementPostRequestDTO userAchievement)
      {

      UserAchievement newUserAchievement = new UserAchievement
        {
        UserAccountId = userAchievement.UserId,
        AchievementId = userAchievement.AchievementId,
        AchievedAt = DateTime.Now
        };

      await _context.UserAchievements.AddAsync(newUserAchievement);
      await _context.SaveChangesAsync();
      return newUserAchievement;
      }

    public async Task<bool> AchievementExistsAsync(int achievementId)
      {
      return await _context.Achievements.AnyAsync(a => a.AchievementId == achievementId);
      }

    }
  }
