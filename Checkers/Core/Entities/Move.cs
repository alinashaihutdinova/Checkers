using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Entities
{
    public class Move
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
        public int MoveNumber { get; set; }
        public string PlayerColor { get; set; } = null!;
        public string FromPosition { get; set; } = null!;
        public string ToPosition { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
