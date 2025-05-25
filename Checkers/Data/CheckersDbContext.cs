using Microsoft.EntityFrameworkCore;
using Checkers.Core.Entities;

namespace Checkers.Data
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    internal class CheckersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=postgres;Password=alshnqq");
        }
    }
}
