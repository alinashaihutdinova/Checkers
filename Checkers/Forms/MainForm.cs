using Checkers.Core.Services;

namespace Checkers.Forms
{
    /// <summary>
    /// основная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly IUserService _userService;
        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="userService">сервис аутентификации пользователей</param>
        public MainForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            LoadGameHistory();
        }
        private void BtnProfile_Click(object sender, EventArgs e)
        {
            /*var profileForm = new ProfileForm(_userService);
            profileForm.Show();
            this.Hide();*/
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            /*
            var gameForm = new GameForm(_userService);
            gameForm.Show();
            this.Hide();
            */
        }
        private void LoadGameHistory()
        {
            /*var history = _userService.GetGameHistory(); 
            foreach (var entry in history)
            {
                var placeLabel = new Label { Text = entry.Place.ToString() };
                var loginLabel = new Label { Text = entry.Login };
                var winsLabel = new Label { Text = entry.Wins.ToString() };
                var lossesLabel = new Label { Text = entry.Losses.ToString() };

                tableLayoutPanelHistory.Controls.Add(placeLabel);
                tableLayoutPanelHistory.Controls.Add(loginLabel);
                tableLayoutPanelHistory.Controls.Add(winsLabel);
                tableLayoutPanelHistory.Controls.Add(lossesLabel);
            }*/
        }

    }
}
