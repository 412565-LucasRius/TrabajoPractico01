using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<UserAchievement> GetAchievementByUsurnameAsync(string username)
        {
            return await _context.UserAchievements
            .Include(u => u.UserAccount) 
            .FirstOrDefaultAsync(u => u.UserAccount.Username == username);
        }

        public async Task<UserAchievement> PostAchievementAsync(UserAchievement newUserAchievement)
        {
            await _context.UserAchievements.AddAsync(newUserAchievement);
            await _context.SaveChangesAsync();
            return newUserAchievement;
        }
    }
}
