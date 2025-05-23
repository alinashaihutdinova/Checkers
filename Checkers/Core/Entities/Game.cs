using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int WhitePlayerId { get; set; }
        public User WhitePlayer { get; set; } = null!;
        public int BlackPlayerId { get; set; }
        public User BlackPlayer { get; set; } = null!;
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string? Winner { get; set; }
        public List<Move> Moves { get; set; } = new();
    }
}
