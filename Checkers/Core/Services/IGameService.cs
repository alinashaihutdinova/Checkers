using Checkers.Core.Entities;

namespace Checkers.Core.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Возвращает список доступных игр
        /// </summary>
        List<Game> GetAvailableGames();

        /// <summary>
        /// Создаёт новую игру
        /// </summary>
        Game CreateGame(Guid whitePlayerId);
        /// <summary>
        /// Присоединяет игрока к существующей игре
        /// </summary>
        bool JoinGame(Guid gameId, Guid blackPlayerId);

        /// <summary>
        /// Сохраняет ход в БД
        /// </summary>
        void SaveMove(Guid gameId, Guid playerId, string from, string to);
        /// <summary>
        /// 
        /// </summary>
        Game GetGameWithMoves(Guid gameId);
        bool IsPlayersTurn(Guid gameId, Guid userId);
        void EndGame(Guid gameId, string winnerColor);
    }
}
