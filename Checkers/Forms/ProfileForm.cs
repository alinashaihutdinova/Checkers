using Checkers.Core.Services;
using Checkers.Core.Entities;
using Checkers.Classes;

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
        }
        private void UpdateLanguage()
        {
            btnChangeLanguage.Text = LanguageManager.GetString("ButtonChangeLanguage");
            btnback.Text = LanguageManager.GetString("ButtonBack");
            lblprofile.Text = LanguageManager.GetString("LabelProfile");
        }
        private void btnback_Click(object sender, EventArgs e)
        {
            Hide();
            var user = new User();
            var mainform = new MainForm(_userService, _gameService, user);
            mainform.Show();
        }

        private void btnChangeLanguage_Click(object sender, EventArgs e)
        {
            string currentLang = Thread.CurrentThread.CurrentUICulture.Name;

            // Меняем язык
            if (currentLang == "ru")
            {
                LanguageManager.SetLanguage("en");
            }
            else
            {
                LanguageManager.SetLanguage("ru");
            }
            // Обновляем текст кнопки
            UpdateLanguage();
        }
    }
}
