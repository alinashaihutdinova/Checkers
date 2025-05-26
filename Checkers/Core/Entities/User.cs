namespace Checkers.Core.Entities
{
    /// <summary>
    /// пользователь приложения, игрок
    /// </summary>
    public class User
    {
        /// <summary>)
        /// айди пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// логин пользователя
        /// </summary>
        public string Login { get; set; } = null!;
        /// <summary>
        /// хэшированный пароль
        /// </summary>
        public string PasswordHash { get; set; } = null!;
        /// <summary>
        /// игры, где пользователь играет за белых
        /// </summary>
        public List<Game> WhiteGames { get; set; } = new();
        /// <summary>
        /// игры, где пользователь играет за чёрных
        /// </summary>
        public List<Game> BlackGames { get; set; } = new();
    }
}
