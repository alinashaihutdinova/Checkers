using Checkers.Data;
using Checkers.Core.Entities;
using Checkers.Core.Services;

namespace Checkers.Services
{
    /// <summary>
    /// Сервис аутентификации пользователя через логин и пароль с хешированием
    /// </summary>
    public class UserService : IUserService
    {
        private readonly CheckersDbContext _context;

        public UserService(CheckersDbContext context)
        {
            _context = context;
        }

        public User? Authenticate(string login, string password)
        {
            var hashedPassword = HashPassword(password);

            return _context.Users
                .FirstOrDefault(u => u.Login == login && u.PasswordHash == hashedPassword);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var sb = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
