namespace Checkers.Core.Entities
{
    /// <summary>
    /// игра между двумя пользователями
    /// </summary>
    public class Game
    {
        /// <summary>
        /// айди игры
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// айди игрока за белых
        /// </summary>
        public Guid WhitePlayerId { get; set; } 
        /// <summary>
        /// игрок за белых
        /// </summary>
        public User WhitePlayer { get; set; } = null!;
        /// <summary>
        /// айди игрока за чёрных
        /// </summary>
        public Guid ? BlackPlayerId { get; set; }
        /// <summary>
        /// игрок за чёрных
        /// </summary>
        public User BlackPlayer { get; set; } = null!;
        /// <summary>
        /// время начала игры
        /// </summary>
        public DateTime StartedAt { get; set; }
        /// <summary>
        /// время окончания игры 
        /// </summary>
        public DateTime? FinishedAt { get; set; }
        /// <summary>
        /// победитель игры, если есть
        /// </summary>
        public string? Winner { get; set; }
        /// <summary>
        /// список ходов в игре
        /// </summary>
        public List<Move> Moves { get; set; } = new();
        public string Status { get; set; } = "Waiting";
    }
}
