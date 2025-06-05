using Checkers.Core.Entities;
using Checkers.Core.Services;
using Checkers.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Services
{
    /// <summary>
    /// сервис, реализующий логику игры
    /// </summary>
    public class GameService : IGameService
    {
        private readonly CheckersDbContext _context;
        /// <summary>
        /// инициализирует новый экземпляр сервиса с указанием контекста бд
        /// </summary>
        public GameService(CheckersDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// возвращает список доступных игр
        /// </summary>
        public List<Game> GetAvailableGames()
        {
            return _context.Games
                .Where(g => g.Status == "Waiting" && g.BlackPlayerId == null)
                .ToList();
        }
        /// <summary>
        /// создаёт новую игру
        /// </summary>
        public Game CreateGame(Guid whitePlayerId)
        {
            if (!_context.Users.Any(u => u.Id == whitePlayerId))
                throw new ArgumentException("Игрок за белых не найден");
            var game = new Game
            {
                Id = Guid.NewGuid(),
                WhitePlayerId = whitePlayerId,
                BlackPlayerId = null,
                StartedAt = DateTime.UtcNow,
                Status = "Waiting"
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }
        /// <summary>
        /// присоединяет игрока к существующей игре
        /// </summary>
        public bool JoinGame(Guid gameId, Guid blackPlayerId)
        {
            if (!_context.Users.Any(u => u.Id == blackPlayerId))
                return false;
            var game = _context.Games
                .FirstOrDefault(g => g.Id == gameId && g.Status == "Waiting");
            if (game == null || game.BlackPlayerId != null)
                return false;
            game.BlackPlayerId = blackPlayerId;
            game.Status = "Active";  
            _context.SaveChanges();
            return true;
        }
        /// <summary>
        /// сохраняет ход в БД
        /// </summary>
        public void SaveMove(Guid gameId, Guid playerId, string from, string to)
        {
            var game = _context.Games.Find(gameId);
            if (game == null) throw new ArgumentException("Игра не найдена");

            string playerColor = game.WhitePlayerId == playerId ? "White" :
                                 game.BlackPlayerId == playerId ? "Black" : "Unknown";
            var move = new Move
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                PlayerColor = playerColor,
                FromPosition = from,
                ToPosition = to,
                MoveNumber = _context.Moves.Count(m => m.GameId == gameId) + 1,
                CreatedAt = DateTime.UtcNow
             };
            _context.Moves.Add(move);
            game.Turn = game.Turn == "White" ? "Black" : "White";
            _context.SaveChanges();
        }
        /// <summary>
        /// получает игру вместе со всеми ходами 
        /// </summary>
        public Game GetGameWithMoves(Guid gameId)
        {
            using (var freshContext = new CheckersDbContext())
            {
                return freshContext.Games
                    .Include(g => g.WhitePlayer)
                    .Include(g => g.BlackPlayer)
                    .Include(g => g.Moves)
                    .FirstOrDefault(g => g.Id == gameId);
            }
        }
        /// <summary>
        /// проверяет очередь хода
        /// </summary>
        public bool IsPlayersTurn(Guid gameId, Guid userId)
        {
            var game = _context.Games.Find(gameId);
            if (game == null) 
                return false;
            if (game.Turn == "White")
                return game.WhitePlayerId == userId;
            else
                return game.BlackPlayerId.HasValue && game.BlackPlayerId.Value == userId;
        }
        /// <summary>
        /// обновляет историю игр в бд
        /// </summary>
        public void AddGameHistory(Guid userId, Guid gameId, bool isWin)
        {
            var gameHistory = new GameHistory
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                GameId = gameId,
                IsWin = isWin,
                PlayedAt = DateTime.UtcNow
            };
            _context.GameHistories.Add(gameHistory);
            _context.SaveChanges();
        }
        /// <summary>
        /// завершает игру и обновляет статистику
        /// </summary>
        public void EndGame(Guid gameId, string winnerColor)
        {
            var game = _context.Games.Find(gameId);
            if (game == null)
                return;
            game.FinishedAt = DateTime.UtcNow;
            game.Winner = winnerColor;
            game.Status = "Finished";
            Guid winnerId = Guid.Empty;
            Guid loserId = Guid.Empty;
            if (winnerColor == "White")
            {
                winnerId = game.WhitePlayerId; 
                loserId = game.BlackPlayerId ?? Guid.Empty;  
            }
            else
            {
                winnerId = game.BlackPlayerId ?? Guid.Empty;
                loserId = game.WhitePlayerId;
            }
            var winner = _context.Users.Find(winnerId);
            var loser = _context.Users.Find(loserId);
            if (winner != null)
            {
                winner.GamesPlayed++;
                winner.Wins++;
                AddGameHistory(winnerId, gameId, true);
            }
            if (loser != null)
            {
                loser.GamesPlayed++;
                loser.Losses++;
                AddGameHistory(loserId, gameId, false);
            }
            _context.SaveChanges();
        }
    }
}
