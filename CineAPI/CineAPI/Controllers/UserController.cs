﻿using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CineAPI.Controllers
  {
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
    {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
      {
      _userService = userService;
      }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
      {

      try
        {
        if (registerRequest == null)
          {
          return BadRequest("User cannot be null");
          }

        await _userService.RegisterAsync(registerRequest);

        return Ok("User registered successfully");
        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        throw;
        }


      }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
      {

      try
        {
        if (await IsValidUser(loginRequest))
          {

          var user = await _userService.GetUserByNameAsync(loginRequest.Username);
          var token = await GenerateJwtToken(user);

          var response = new LoginResponseDTO
            {
            UserId = user.UserAccountId.ToString(),
            Username = loginRequest.Username,
            Token = token
            };

          return Ok(response);
          }

        return Unauthorized("Invalid username or password");

        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        throw;
        }
      }

    [HttpGet("byid")]
    public async Task<IActionResult> GetUserById([FromQuery] int userAccountId)
      {
      try
        {
        if (userAccountId == 0)
          {
          return BadRequest("Usuario invalido");
          }

        var userData = await _userService.GetUserAccountById(userAccountId);

        return Ok(userData);
        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        throw;
        }
      }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserData([FromBody] UserAccount userData)
      {
      try
        {
        if (string.IsNullOrEmpty(userData.Username))
          {
          return BadRequest("El nombre de usuario no puede estar vacío.");
          }

        var userUpdated = await _userService.UpdateUserData(userData);
        if (userUpdated != null)
          {
          return Ok(userUpdated);
          }
        else
          {
          return BadRequest("No se pudo actualizar el nombre de usuario.");
          }
        }
      catch (Exception ex)
        {
        Console.WriteLine(ex.Message);
        return StatusCode(500, "Ocurrió un error en el servidor.");
        }
      }

    private async Task<bool> IsValidUser(LoginRequestDTO loginRequest)
      {
      var user = await _userService.GetUserByNameAsync(loginRequest.Username);

      if (user == null)
        return false;

      return BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
      }

    private async Task<string> GenerateJwtToken(UserAccount user)
      {
      var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userId", user.UserAccountId.ToString())
        };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hRF0mGEuNcldIPzK4skzQ4RdhLWlDsW5bs4Gn7aio2w="));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: "*",
          audience: "*",
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: credentials
        );

      return new JwtSecurityTokenHandler().WriteToken(token);
      }
    }
  }
