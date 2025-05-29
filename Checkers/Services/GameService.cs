using Checkers.Core.Entities;
using Checkers.Core.Services;
using Checkers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Where(g => g.BlackPlayerId == Guid.Empty)
                .ToList();
        }
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
        public bool JoinGame(Guid gameId, Guid blackPlayerId)
        {
            var game = _context.Games.Find(gameId);
            if (game is null || game.BlackPlayerId != Guid.Empty)
                return false;

            game.BlackPlayerId = blackPlayerId;
            _context.SaveChanges();
            return true;
        }
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
    }
}
