using Checkers.Core.Services;
using Checkers.Core.Entities;
using Checkers.Classes;
using NLog;

namespace Checkers.Forms
{
    /// <summary>
    /// форма профиля пользователя
    /// </summary>
    public partial class ProfileForm : Form
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly User _user;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// конструктор класса 
        /// </summary>
        public ProfileForm(IUserService userService, User user)
        {
            InitializeComponent();
            _userService = userService;
            _user = user;
            LanguageManager.OnLanguageChanged += UpdateLanguage;
            UpdateLanguage();
            lblLogin.Text = _user.Login;
            lblGames.Text = $"Игры: {_user.GamesPlayed}";
            lblWins.Text = $"Победы: {_user.Wins}";
            lblLosses.Text = $"Поражения: {_user.Losses}";
            _logger.Info($"Форма профиля открыта для пользователя: {_user.Login}");
        }
        private void UpdateLanguage()
        {
            btnChangeLanguage.Text = LanguageManager.GetString("ButtonChangeLanguage");
            btnback.Text = LanguageManager.GetString("ButtonBack");
            lblprofile.Text = LanguageManager.GetString("LabelProfile");
            lblGames.Text = $"{LanguageManager.GetString("LabelGames")}: {_user.GamesPlayed}";
            lblWins.Text = $"{LanguageManager.GetString("LabelWins")}: {_user.Wins}";
            lblLosses.Text = $"{LanguageManager.GetString("LabelLosses")}: {_user.Losses}";
            _logger.Debug("Язык интерфейса обновлён");
        }
        
        private void btnback_Click(object sender, EventArgs e)
        {
            _logger.Debug("Пользователь вернулся в главную форму");
            Hide();
            var mainform = new MainForm(_userService, _gameService, _user);
            mainform.Show();
        }
        private void btnChangeLanguage_Click(object sender, EventArgs e)
        {
            string currentLang = Thread.CurrentThread.CurrentUICulture.Name;
            _logger.Debug($"Текущий язык: {currentLang}");
            if (currentLang == "ru")
            {
                LanguageManager.SetLanguage("en");
                _logger.Info("Язык изменён на английский");
            }
            else
            {
                LanguageManager.SetLanguage("ru");
                _logger.Info("Язык изменён на русский");
            }
            UpdateLanguage();
        }
    }
}
