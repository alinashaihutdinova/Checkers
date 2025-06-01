using Checkers.Core.Services;
using Checkers.Core.Entities;

namespace Checkers.Forms
{
    /// <summary>
    /// форма профиля пользователя
    /// </summary>
    public partial class ProfileForm : Form
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        /// <summary>
        /// конструктор класса 
        /// </summary>
        public ProfileForm()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Hide();
            var user = new User();
            var mainform = new MainForm(_userService, _gameService, user);
            mainform.Show();
        }
    }
}
