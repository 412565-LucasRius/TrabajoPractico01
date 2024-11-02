using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CineRepository.Repositories.Implementations
  {
  public class UserRepository : IUserRepository
    {

    private readonly Cine_1W3_TPContext _context;

    public UserRepository(Cine_1W3_TPContext context)
      {
      _context = context;
      }

    public async Task<IEnumerable<UserAccount>> GetAllUsersAsync()
      {
      return await _context.UserAccounts.ToListAsync();
      }

    public async Task<UserAccount> GetUserByNameAsync(string username)
      {
      return await _context.UserAccounts
        .Include(u => u.Customer)
        .FirstOrDefaultAsync(u => u.Username == username);
      }

    public async Task RegisterAsync(UserAccount newUser)
      {
      await _context.UserAccounts.AddAsync(newUser);
      await _context.SaveChangesAsync();
      }
    }
  }
