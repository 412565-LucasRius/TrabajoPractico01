using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using CineRepository.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CineRepository.Services.Implementations
  {
  public class UserService : IUserService
    {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository repository)
      {
      _userRepository = repository;
      }

    public async Task<UserAccount> AuthenticateAsync(string username, string password)
      {
      var userAccount = await _userRepository.GetUserByNameAsync(username);

      if (userAccount == null)
        return null;

      if (!VerifyPasswordHash(password, userAccount.PasswordHash))
        return null;


      return userAccount;
      }


    public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
      {
      return await _userRepository.GetAllUsersAsync();
      }

    public async Task RegisterAsync(UserAccount newUser)
      {

      if (string.IsNullOrWhiteSpace(newUser.Username) || string.IsNullOrWhiteSpace(newUser.PasswordHash))
        {
        throw new ArgumentException("Username and password are required");
        }

      if (newUser.PasswordHash.Length < 6)
        {
        throw new ArgumentException("Password must be at least 6 characters long.");
        }

      //if (!IsValidEmail(email))
      //  {
      //  throw new ArgumentException("Invalid email format.");
      //  }

      newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);
      await _userRepository.RegisterAsync(newUser);
      }


    private bool VerifyPasswordHash(string password, string passwordHash)
      {
      return BCrypt.Net.BCrypt.Verify(password, passwordHash);
      }

    private bool IsValidEmail(string email)
      {
      return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
      }
    }
  }
