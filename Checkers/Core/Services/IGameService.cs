using Checkers.Core.Entities;

namespace Checkers.Core.Services
{
    public interface IGameService
    {
        List<Game> GetAvailableGames();
        Game CreateGame(Guid whitePlayerId);
        bool JoinGame(Guid gameId, Guid blackPlayerId);
        void SaveMove(Guid gameId, Guid playerId, string from, string to);
        Game GetGameWithMoves(Guid gameId);
        bool IsPlayersTurn(Guid gameId, Guid userId);
        void EndGame(Guid gameId, string winnerColor);
        void AddGameHistory(Guid userId, Guid gameId, bool isWin);
    }
}
