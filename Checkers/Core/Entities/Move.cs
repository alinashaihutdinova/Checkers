
namespace Checkers.Core.Entities
{
    /// <summary>
    /// ход в игре
    /// </summary>
    public class Move
    {
        /// <summary>
        /// айди хода
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// айди игры, к которой относится ход
        /// </summary>
        public Guid GameId { get; set; }
        /// <summary>
        /// ссылка на игру
        /// </summary>
        public Game Game { get; set; } = null!;
        /// <summary>
        /// номер хода
        /// </summary>
        public int MoveNumber { get; set; }
        /// <summary>
        /// цвет игрока, сделалавшего ход
        /// </summary>
        public string PlayerColor { get; set; } = null!;
        /// <summary>
        /// позиция, откуда был сделан ход
        /// </summary>
        public string FromPosition { get; set; } = null!;
        /// <summary>
        /// позиция, куда был сделан ход
        /// </summary>
        public string ToPosition { get; set; } = null!;
        /// <summary>
        /// время создания хода
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
