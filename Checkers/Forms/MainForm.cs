using Checkers.Core.Services;
using NLog;

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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// конструктор класса
        /// </summary>
        public MainForm(IUserService userService, IGameService gameService, Core.Entities.User user)
        {
            InitializeComponent();
            _userService = userService;
            _gameService = gameService;
            _user = user;
            LoadRatingTable();
            _logger.Info($"Пользователь {_user.Login} вошёл в главное меню");
        }
        private void BtnProfile_Click(object sender, EventArgs e)
        {
            _logger.Debug("Переход к профилю");
            var profileForm = new ProfileForm(_userService, _user);
            profileForm.Show();
            this.Hide();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            _logger.Info("Выход из приложения");
            Application.Exit();
        }
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            _logger.Debug("Создание новой игры");
            var game = _gameService.CreateGame(_user.Id);
            _logger.Info($"Игра создана: {game.Id}, Белый игрок: {_user.Id}");
            var form = new GameForm(_userService, _gameService, game.Id, isWhite: true, currentUserId: _user.Id);
            form.FormClosed += formClosed;
            form.Show();
            this.Hide();
        }
        private void LoadRatingTable()//метод для заполнения таблицы рейтинга
        {
            _logger.Debug("Загрузка рейтинговой таблицы");
            var users = _userService.GetAllUsersSortedByRating();
            while (tblLayoutPnlHistory.RowCount > 1)
            {
                for (int j = 0; j < 4; j++)
                {
                    var control = tblLayoutPnlHistory.GetControlFromPosition(j, 1);
                    if (control != null)
                    {
                        tblLayoutPnlHistory.Controls.Remove(control);
                        control.Dispose();
                    }
                }
                tblLayoutPnlHistory.RowCount--;
                tblLayoutPnlHistory.RowStyles.RemoveAt(1);
            }
            var row = 1;
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
                tblLayoutPnlHistory.RowCount++;
                tblLayoutPnlHistory.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tblLayoutPnlHistory.Controls.Add(placeLabel, 0, row);
                tblLayoutPnlHistory.Controls.Add(loginLabel, 1, row);
                tblLayoutPnlHistory.Controls.Add(winsLabel, 2, row);
                tblLayoutPnlHistory.Controls.Add(lossesLabel, 3, row);
                row++;
            }
            _logger.Info($"Рейтинговая таблица обновлена. Найдено пользователей: {users.Count}");
        }
        private void formClosed(object sender, FormClosedEventArgs e)
        {
            _logger.Debug("Форма игры закрыта. Обновление таблицы");
            LoadRatingTable();
        }
        private void btnjoingame_Click(object sender, EventArgs e)
        {
            _logger.Debug("Поиск доступных игр");
            var availableGames = _gameService.GetAvailableGames();
            if (!availableGames.Any())
            {
                _logger.Warn("Нет доступных игр для присоединения");
                MessageBox.Show("нет доступных игр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var gameToJoin = availableGames.First();
            _logger.Debug($"Попытка присоединиться к игре ID: {gameToJoin.Id}");
            bool joined = _gameService.JoinGame(gameToJoin.Id, _user.Id);

            if (joined)
            {
                _logger.Info($"Пользователь {_user.Login} присоединился к игре ID: {gameToJoin.Id}");
                var form = new GameForm(_userService, _gameService, gameToJoin.Id, isWhite: false, currentUserId: _user.Id);
                form.Show();
                this.Hide();
            }
            else
            {
                _logger.Error($"Не удалось присоединиться к игре ID: {gameToJoin.Id}");
                MessageBox.Show("не удалось присоединиться к игре", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
