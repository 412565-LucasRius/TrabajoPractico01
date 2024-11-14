using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IAchievementsRepository
    {
    public Task<IEnumerable<Achievement>> GetAchievementsAsync();

    }
  }
