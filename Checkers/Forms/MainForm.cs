using Checkers.Core.Services;

namespace Checkers.Forms
{
    /// <summary>
    /// основная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly Core.Entities.User _user;
        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="userService">сервис аутентификации пользователей</param>
        public MainForm(IUserService userService, IGameService gameService, Core.Entities.User user)
        {
            InitializeComponent();
            _userService = userService;
            _gameService = gameService;
            _user = user;
            LoadRatingTable();
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
            var game = _gameService.CreateGame(_user.Id);
            var form = new GameForm(_userService, _gameService, game.Id, isWhite: true, currentUserId: _user.Id);
            form.Show();
            this.Hide();
        }
        private void LoadRatingTable()
        {
            var users = _userService.GetAllUsersSortedByRating();
            var currentRowCount = tblLayoutPnlHistory.RowCount;
            for (int i = currentRowCount - 1; i >= 1; i--)
            {
                // Удаляем контроллы из строк данных
                for (int j = 0; j < 4; j++)
                {
                    if (tblLayoutPnlHistory.GetControlFromPosition(j, i) is Control control)
                    {
                        tblLayoutPnlHistory.Controls.Remove(control);
                        control.Dispose();
                    }
                }
                // Удаляем стиль строки
                tblLayoutPnlHistory.RowStyles.RemoveAt(i);
            }
            // Теперь добавляем новые строки
            int row = 1;
            foreach (var user in users)
            {
                var placeLabel = new Label
                {
                    Text = row.ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    AutoSize = false
                };
                var loginLabel = new Label
                {
                    Text = user.Login,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    AutoSize = false
                };
                var winsLabel = new Label
                {
                    Text = user.Wins.ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    AutoSize = false
                };
                var lossesLabel = new Label
                {
                    Text = user.Losses.ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    AutoSize = false
                };
                // Добавляем новую строку
                tblLayoutPnlHistory.RowCount++;
                tblLayoutPnlHistory.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                // Добавляем контроллы
                tblLayoutPnlHistory.Controls.Add(placeLabel, 0, row);
                tblLayoutPnlHistory.Controls.Add(loginLabel, 1, row);
                tblLayoutPnlHistory.Controls.Add(winsLabel, 2, row);
                tblLayoutPnlHistory.Controls.Add(lossesLabel, 3, row);
                row++;
            }

        }

        private void btnjoingame_Click(object sender, EventArgs e)
        {
            var availableGames = _gameService.GetAvailableGames();
            if (!availableGames.Any())
            {
                MessageBox.Show("Нет доступных игр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var gameToJoin = availableGames.First(); // Можно выбрать любую, но пока первую
            bool joined = _gameService.JoinGame(gameToJoin.Id, _user.Id);

            if (joined)
            {
                var form = new GameForm(_userService, _gameService, gameToJoin.Id, isWhite: false, currentUserId: _user.Id);
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Не удалось присоединиться к игре", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
