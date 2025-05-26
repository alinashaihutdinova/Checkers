using Microsoft.EntityFrameworkCore;
using Checkers.Core.Entities;

namespace Checkers.Data
{
    /// <summary>
    /// контекст базы данных
    /// </summary>
    public class CheckersDbContext : DbContext
    {
        /// <summary>
        /// таблица "users" в БД
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// таблица "games" в БД
        /// </summary>
        public DbSet<Game> Games { get; set; }
        /// <summary>
        /// таблица "moves" в БД
        /// </summary>
        public DbSet<Move> Moves { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=postgres;Password=alshnqq");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<Move>().HasKey(m => m.Id);
            modelBuilder.Entity<Game>()
                .HasOne(g => g.WhitePlayer)
                .WithMany(u => u.WhiteGames)
                .HasForeignKey(g => g.WhitePlayerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Game>()
                .HasOne(g => g.BlackPlayer)
                .WithMany(u => u.BlackGames)
                .HasForeignKey(g => g.BlackPlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
