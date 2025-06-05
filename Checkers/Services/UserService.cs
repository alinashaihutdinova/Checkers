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
        /// <summary>
        /// возвращает историю игр пользователя
        /// </summary>
        public List<Game> GetUserGameHistory(Guid userId) =>
            _context.Games
                .Where(g => g.WhitePlayerId == userId || g.BlackPlayerId == userId)
                .ToList();
        /// <summary>
        /// возвращает список пользователей, отсортированных по рейтингу
        /// </summary>
        public List<User> GetAllUsersSortedByRating()
        {
            return _context.Users
                .AsEnumerable()
                .OrderByDescending(u => u.Wins * 50 - u.Losses * 30) 
                .ToList();
        }
        /// <summary>
        /// считает рейтинг
        /// </summary>
        public int CalculateRating(User user)
        {
            return user.Wins * 50 - user.Losses * 30;
        }
        /// <summary>
        /// обновляет статистику пользователя после игры
        /// </summary>
        public void UpdateUserStats(Guid userId, bool isWin)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return;
            user.GamesPlayed++;
            if (isWin)
            {
                user.Wins++;
            }
            else
            {
                user.Losses++;
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// находит пользователя по айди
        /// </summary>
        public User GetUserById(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new ArgumentException("Пользователь не найден", nameof(id));
            return user;
        }
    }
}
