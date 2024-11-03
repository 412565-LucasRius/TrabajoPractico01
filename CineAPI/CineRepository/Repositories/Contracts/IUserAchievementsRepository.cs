using CineRepository.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineRepository.Repositories.Contracts
{
    public interface IUserAchievementsRepository
    {
        Task<UserAchievement> GetAchievementByUsurnameAsync(string usurname);
        Task<UserAchievement> PostAchievementAsync(UserAchievement newUserAchievement);

    }
}
