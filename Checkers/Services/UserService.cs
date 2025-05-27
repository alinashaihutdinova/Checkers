using Checkers.Data;
using Checkers.Core.Entities;
using Checkers.Core.Services;

namespace Checkers.Services
{
    /// <summary>
    /// cервис аутентификации пользователя через логин и пароль с хешированием
    /// </summary>
    public class UserService : IUserService
    {
        private readonly CheckersDbContext _context;
        /// <summary>
        /// инициализирует новый экземпляр сервиса с указанием контекста бд
        /// </summary>
        public UserService(CheckersDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// аутентифицирует пользователя по его логину и паролю
        /// </summary>
        public User? Authenticate(string login, string password)
        {
            var hashedPassword = HashPassword(password);

            return _context.Users
                .FirstOrDefault(u => u.Login == login && u.PasswordHash == hashedPassword);
        }
        /// <summary>
        /// хэширует пароль 
        /// </summary>
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
        /// <summary>
        /// сохраняет пользователя в бд
        /// </summary>
        public void RegisterUser(User user)
        {
            if (_context.Users.Any(u => u.Login == user.Login))
                throw new Exception("Пользователь с таким логином уже существует");

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
