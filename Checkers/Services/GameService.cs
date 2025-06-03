using Checkers.Core.Entities;
using Checkers.Core.Services;
using Checkers.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Services
{
    public class GameService : IGameService
    {
        private readonly CheckersDbContext _context;
        public GameService(CheckersDbContext context)
        {
            _context = context;
        }
        public List<Game> GetAvailableGames()
        {
            return _context.Games
                .Where(g => g.Status == "Waiting" && g.BlackPlayerId == null)
                .ToList();
        }
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
        public void SaveMove(Guid gameId, Guid playerId, string from, string to)
        {
            var game = _context.Games
                .Include(g => g.WhitePlayer)
                .Include(g => g.BlackPlayer)
                .FirstOrDefault(g => g.Id == gameId);
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
            _context.SaveChanges();
        }
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
        public bool IsPlayersTurn(Guid gameId, Guid userId)
        {
            var game = _context.Games.Find(gameId);
            if (game == null) return false;

            var moveCount = _context.Moves.Count(m => m.GameId == gameId);
            var isWhiteTurn = moveCount % 2 == 0;

            return (isWhiteTurn && game.WhitePlayerId == userId) ||
                   (!isWhiteTurn && game.BlackPlayerId == userId);
        }
        public void EndGame(Guid gameId, string winnerColor)
        {
            var game = _context.Games.Find(gameId);
            if (game == null)
                return;
            game.FinishedAt = DateTime.UtcNow;
            game.Winner = winnerColor;
            game.Status = "Finished";
            Guid winnerId, loserId;
            if (winnerColor == "White")
            {
                winnerId = game.WhitePlayerId; 
                loserId = game.BlackPlayerId ?? Guid.Empty;  
            }
            else
            {
                winnerId = game.BlackPlayerId.Value; 
                loserId = game.WhitePlayerId; 
            }
            var winner = _context.Users.Find(winnerId);
            var loser = _context.Users.Find(loserId);
            if (winner != null)
            {
                winner.GamesPlayed++;
                winner.Wins++;
            }

            if (loser != null)
            {
                loser.GamesPlayed++;
                loser.Losses++;
            }
            _context.SaveChanges();
        }
    }
}
