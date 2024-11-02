using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IUserService
    {
    Task<UserAccount> AuthenticateAsync(string username, string password);
    Task RegisterAsync(UserAccount newUser);
    Task<IEnumerable<UserAccount>> GetAllUsersAsync();
    }
  }
