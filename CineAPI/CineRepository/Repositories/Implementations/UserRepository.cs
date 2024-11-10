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

    public async Task<UserResponseDTO> GetUserAccountById(int userAccountId)
      {
      var userResponse = await _context.UserAccounts
        .Where(ua => ua.UserAccountId == userAccountId)
        .Join(
          _context.Customers,
          ua => ua.CustomerId,
          cu => cu.CustomerId,
          (ua, cu) => new { ua, cu })
        .Join(
            _context.Contacts,
            combined => combined.cu.CustomerId,
            co => co.CustomerId,
            (combined, co) => new { combined.ua, combined.cu, co })
        .Join(
            _context.ContactTypes,
            combined => combined.co.ContactTypeId,
            ct => ct.ContactTypeId,
            (combined, ct) => new { combined.ua, combined.cu, combined.co, ct })
        .Where(result => result.ct.ContactTypeId == 2)
        .Select(result => new UserResponseDTO
          {
          UserId = result.ua.UserAccountId,
          Name = result.cu.Name,
          Username = result.ua.Username,
          Email = result.co.Contact1
          })
        .FirstOrDefaultAsync();

      return userResponse;
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

    public async Task UpdateLastLoginAsync(UserAccount userAccount)
      {
      userAccount.LastLogin = DateTime.Now;
      await _context.SaveChangesAsync();
      }

    public async Task<UserAccount> UpdateUsernameAsync(int userAccountId, string newUsername)
      {
      var user = await _context.UserAccounts.FindAsync(userAccountId);
      if (user == null)
        return null;

      var isUsernameTaken = await _context.UserAccounts.
        AnyAsync(u => u.Username == newUsername && u.UserAccountId != userAccountId);

      if (isUsernameTaken)
        {
        throw new Exception("El nombre de usuario ya está en uso.");
        }

      user.Username = newUsername;
      await _context.SaveChangesAsync();

      return user;
      }
    }
  }
