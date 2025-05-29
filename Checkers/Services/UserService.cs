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
        /// Получает список доступных игр (где нет черного игрока)
        /// </summary>
        public List<Game> GetAvailableGames() =>
            _context.Games.Where(g => g.BlackPlayerId == Guid.Empty).ToList();
        /// <summary>
        /// Создаёт новую игру за белых
        /// </summary>
        public Game CreateGame(Guid whitePlayerId)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                WhitePlayerId = whitePlayerId,
                StartedAt = DateTime.Now
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }
        /// <summary>
        /// Присоединяет игрока к существующей игре за черных
        /// </summary>
        public bool JoinGame(Guid gameId, Guid blackPlayerId)
        {
            var game = _context.Games.Find(gameId);
            if (game is null || game.BlackPlayerId != Guid.Empty || game.WhitePlayerId == blackPlayerId)
                return false;

            game.BlackPlayerId = blackPlayerId;
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// Сохраняет ход в БД
        /// </summary>
        public void SaveMove(Guid gameId, Guid playerId, string from, string to)
        {
            var move = new Move
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                PlayerColor = _context.Users.Find(playerId)?.Login ?? "Unknown",
                FromPosition = from,
                ToPosition = to,
                CreatedAt = DateTime.Now
            };

            _context.Moves.Add(move);
            _context.SaveChanges();
        }
        /// <summary>
        /// Возвращает историю игр пользователя
        /// </summary>
        public List<Game> GetUserGameHistory(Guid userId) =>
            _context.Games
                .Where(g => g.WhitePlayerId == userId || g.BlackPlayerId == userId)
                .ToList();
        /// <summary>
        /// Возвращает список пользователей, отсортированных по рейтингу (по убыванию)
        /// </summary>
        public List<User> GetAllUsersSortedByRating()
        {
            return _context.Users
                .OrderByDescending(u => u.Rating)
                .ToList();
        }
        /// <summary>
        /// Обновляет статистику пользователя после игры
        /// </summary>
        public void UpdateUserStats(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
