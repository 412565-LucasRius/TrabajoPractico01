using CineRepository.Models.DTO;
using CineRepository.Models.Entities;

namespace CineRepository.Services.Interfaces
  {
  public interface IUserService
    {
    Task<UserAccount> AuthenticateAsync(string username, string password);
    Task RegisterAsync(RegisterRequestDTO registerRequest);
    Task<IEnumerable<UserAccount>> GetAllUsersAsync();
    Task<UserAccount> GetUserByNameAsync(string username);
    Task<UserResponseDTO> GetUserAccountById(int userAccountId);
    Task<UserAccount> UpdateUserData(UserAccountUpdateRequestDTO userData);
    Task<bool> DeactivateUser(int authenticatedUserId, int userId);
    }
  }
