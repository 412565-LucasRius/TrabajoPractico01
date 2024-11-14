using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CineRepository.Repositories.Implementations
  {
  public class AchievementsRepository : IAchievementsRepository
    {
    private readonly Cine_1W3_TPContext _context;
    public AchievementsRepository(Cine_1W3_TPContext context)
      {
      _context = context;
      }

    public async Task<IEnumerable<Achievement>> GetAchievementsAsync()
      {
      return await _context.Achievements.ToListAsync();
      }
    }
  }
