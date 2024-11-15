using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IAchievementsService
    {

    public Task<IEnumerable<Achievement>> GetAchievementsAsync();

    }
  }
