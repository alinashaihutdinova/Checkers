
namespace Checkers.Core.Entities
{
    public class GameHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public bool IsWin { get; set; }
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public Game Game { get; set; } = null!;
    }
}
