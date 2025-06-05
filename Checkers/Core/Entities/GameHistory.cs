
namespace Checkers.Core.Entities
{
    /// <summary>
    /// история игры пользователя
    /// </summary>
    public class GameHistory
    {
        /// <summary>
        /// айди записи в истории игр
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// айди пользователя участвовавшего в игре
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// айди игры, к которой относится запись
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// указывает, выиграл ли пользователь эту игру
        /// </summary>
        public bool IsWin { get; set; }
        /// <summary>
        /// дата и время, когда игра была завершена
        /// </summary>
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// ссылка на пользователя участвовавшего в игре
        /// </summary>
        public User User { get; set; } = null!;
        /// <summary>
        /// ссылка на игру
        /// </summary>
        public Game Game { get; set; } = null!;
    }
}
