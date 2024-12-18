﻿using CineRepository.Models.DTO;
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

      if (!userAccount.IsActive)
        {
        userAccount.IsActive = false;
        return userAccount;
        }

      if (!VerifyPasswordHash(password, userAccount.PasswordHash))
        return null;

      await _userRepository.UpdateLastLoginAsync(userAccount);

      return userAccount;
      }


    public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
      {
      return await _userRepository.GetAllUsersAsync();
      }

    public async Task RegisterAsync(RegisterRequestDTO registerRequest)
      {
      
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

    public Task<UserResponseDTO> GetUserAccountById(int userAccountId)
      {
      return _userRepository.GetUserAccountById(userAccountId);
      }

    public async Task<UserAccount> UpdateUserData(UserAccountUpdateRequestDTO userData)
      {
      if (string.IsNullOrEmpty(userData.NewUsername))
        {
        throw new ArgumentException("El nombre de usuario no puede estar vacío.");
        }

      return await _userRepository.UpdateUsernameAsync(userData.UserAccountId, userData.NewUsername);
      }

    public async Task<bool> DeactivateUser(int authenticatedUserId, int userId)
      {
      if (authenticatedUserId != userId)
        {
        return false;
        }

      return await _userRepository.DeactivateUserAsync(userId);
      }
    }
  }
