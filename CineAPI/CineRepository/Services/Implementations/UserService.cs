using CineRepository.Models.DTO;
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

    public async Task RegisterAsync(RegisterRequestDTO registerRequest)
      {
      // Validaciones
      if (string.IsNullOrWhiteSpace(registerRequest.Username) || string.IsNullOrWhiteSpace(registerRequest.Password))
        {
        throw new ArgumentException("Username and password are required");
        }

      if (registerRequest.Password.Length < 6)
        {
        throw new ArgumentException("Password must be at least 6 characters long.");
        }

      if (!IsValidEmail(registerRequest.Email))
        {
        throw new ArgumentException("Invalid email format.");
        }

      var newUser = new UserAccount
        {
        Username = registerRequest.Username,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
        CreatedAt = DateTime.UtcNow,
        LastLogin = DateTime.UtcNow,
        Customer = new Customer
          {
          Name = registerRequest.Name,
          BornDate = registerRequest.BornDate,
          Retired = false,
          Contacts = new List<Contact>
            {
                new Contact
                {
                    ContactTypeId = 2,
                    Contact1 = registerRequest.Email
                }
            }
          }
        };

      // Llamar al repositorio para registrar
      await _userRepository.RegisterAsync(newUser);
      }



    private bool VerifyPasswordHash(string password, string passwordHash)
      {
      return BCrypt.Net.BCrypt.Verify(password, passwordHash);
      }

    private static bool IsValidEmail(string email)
      {
      return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
      }

    public Task<UserAccount> GetUserByNameAsync(string username)
      {
      return _userRepository.GetUserByNameAsync(username);
      }
    }
  }
