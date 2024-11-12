using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;

namespace CineRepository.Services.Implementations
  {
  public class AchievementsService : IAchievementsService
    {
    private readonly IAchievementsRepository _achievementsRepository;

    public AchievementsService(IAchievementsRepository repository)
      {
      _achievementsRepository = repository;
      }
    public async Task<IEnumerable<Achievement>> GetAchievementsAsync()
      {
      return await _achievementsRepository.GetAchievementsAsync();
      }

    }
  }
