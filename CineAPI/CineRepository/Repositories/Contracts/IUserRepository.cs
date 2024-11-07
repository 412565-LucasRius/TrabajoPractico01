using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Repositories.Contracts
  {
  public interface IUserRepository
    {
    Task RegisterAsync(UserAccount newUser);
    Task<UserAccount> GetUserByNameAsync(string username);
    Task<IEnumerable<UserAccount>> GetAllUsersAsync();
    Task<string> GetUserEmailAsync(int userAccountId);

    Task<UserResponseDTO> GetUserAccountById(int userAccountId);
    }
  }
