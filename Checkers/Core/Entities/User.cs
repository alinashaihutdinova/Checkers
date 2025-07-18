﻿namespace Checkers.Core.Entities
{
    /// <summary>
    /// пользователь приложения, игрок
    /// </summary>
    public class User
    {
        /// <summary>)
        /// айди пользователя
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// логин пользователя
        /// </summary>
        public string Login { get; set; } = null!;
        /// <summary>
        /// хэшированный пароль
        /// </summary>
        public string PasswordHash { get; set; } = null!;
        
        /// <summary>
        ///колво сыгранных игр
        /// </summary>
        public int GamesPlayed { get; set; }
        /// <summary>
        /// колво выигрышей
        /// </summary>
        public int Wins { get; set; }
        /// <summary>
        /// колво проигрышей
        /// </summary>
        public int Losses { get; set; }
        /// <summary>
        /// игры, где пользователь играет за белых
        /// </summary>
        public List<Game> WhiteGames { get; set; } = new();
        /// <summary>
        /// игры, где пользователь играет за чёрных
        /// </summary>
        public List<Game> BlackGames { get; set; } = new();
        /// <summary>
        /// история игр
        /// </summary>
        public List<GameHistory> GameHistories { get; set; } = new();
    }
}
