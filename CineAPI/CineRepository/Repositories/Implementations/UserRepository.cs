using CineRepository.Models.DTO;
using CineRepository.Models.Entities;
using CineRepository.Repositories.Contracts;
using Microsoft.Data.SqlClient;
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

    public async Task<string> GetUserEmailAsync(int userAccountId)
      {
      var emailResults = await _context.Set<EmailResultDTO>()
        .FromSqlRaw("SP_GET_USER_EMAIL @UserAccountId", new SqlParameter("@UserAccountId", userAccountId))
        .ToListAsync();

      return emailResults.FirstOrDefault()?.Email;
      }

    public async Task RegisterAsync(UserAccount newUser)
      {
      var emailContact = newUser.Customer.Contacts.FirstOrDefault(c => c.ContactTypeId == 2);

      // Asegúrate de que emailContact no sea nulo
      if (emailContact == null)
        {
        throw new ArgumentException("Email contact is required.");
        }

      await _context.Database.ExecuteSqlRawAsync(
          "EXEC SP_REGISTER_USER @Username, @PasswordHash, @CustomerName, @BornDate, @Retired, @Email",
          new SqlParameter("@Username", newUser.Username),
          new SqlParameter("@PasswordHash", newUser.PasswordHash),
          new SqlParameter("@CustomerName", newUser.Customer.Name),
          new SqlParameter("@BornDate", newUser.Customer.BornDate),
          new SqlParameter("@Retired", newUser.Customer.Retired),
          new SqlParameter("@Email", emailContact.Contact1)
      );
      }
    }
  }
