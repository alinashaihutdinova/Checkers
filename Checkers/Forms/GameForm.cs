using Checkers.Classes;
using Checkers.Core.Entities;
using Checkers.Core.Services;
using NLog;

namespace Checkers.Forms
{
    /// <summary>
    /// форма для игры в шашки
    /// </summary>
    public partial class GameForm : Form
    {
        public Checker SelectedChecker { get => _selectedChecker; set => _selectedChecker = value; }
        public List<(int, int)> AvailableMoves { get => _availableMoves; set => _availableMoves = value; }
        public bool CurrentPlayerIsWhite { get => _currentPlayerIsWhite; set => _currentPlayerIsWhite = value; }
        public int TimeLeft { get => _timeLeft; set => _timeLeft = value; }
        public System.Windows.Forms.Timer Timer { get => _timer; set => _timer = value; }

        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly Core.Entities.User _user;
        private readonly Guid _gameId;
        private readonly bool _isWhite;
        private Guid CurrentUserId { get; set; }
        private Board _board = new();
        private Checker? _selectedChecker;
        private List<(int, int)> _availableMoves = new ();
        private int _timeLeft = 30;
        private System.Windows.Forms.Timer _timer;
        private bool _currentPlayerIsWhite = true;
        private int _lastMoveCount = 0;
        private System.Windows.Forms.Timer _dbPollingTimer;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// конструктор формы
        /// </summary>
        public GameForm(IUserService userService, IGameService gameService, Guid gameId, bool isWhite, Guid currentUserId, Core.Entities.User user)
        {
            InitializeComponent();
            _userService = userService;
            _gameService = gameService;
            _gameId = gameId;
            _isWhite = isWhite;
            CurrentUserId = currentUserId;
            _user = user;
            InitializeBoard();
            LoadAndStartGame();
            InitializeTimers();
            LanguageManager.OnLanguageChanged += UpdateLanguage;
            UpdateLanguage();
            _logger.Info($"Игровая форма запущена. Игра ID: {_gameId}, Цвет игрока: {(_isWhite ? "Белый" : "Чёрный")}");
        }
        private void UpdateLanguage()
        {
            btngiveup.Text = LanguageManager.GetString("ButtonGiveUp");
            btnendmove.Text = LanguageManager.GetString("ButtonEndMove");
            UpdatePlayerInfo();
        }
        private void InitializeBoard()
        {
            _board = new Board();
            _board.InitializeBoard();
            _logger.Debug("Доска инициализирована");
        }
        private void InitializeTimers()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += MoveTimer;

            _dbPollingTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            _dbPollingTimer.Tick += RefreshBoardIfUpdated;
            _dbPollingTimer.Start();
            _logger.Debug("Таймеры запущены");
            CheckAndStartMoveTimer();
        }
        private void LoadAndStartGame()
        {
            UpdatePlayerInfo();
            LoadGameFromDatabase();
            var game = _gameService.GetGameWithMoves(_gameId);
            _lastMoveCount = game.Moves.Count;
            _logger.Info($"Игра загружена. Количество ходов: {_lastMoveCount}");
        }
        public void btngiveup_Click(object sender, EventArgs e)
        {
            _logger.Warn($"Пользователь сдался. Игра ID: {_gameId}");
            var result = MessageBox.Show("Вы действительно хотите сдаться?","Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _gameService.EndGame(_gameId, _isWhite ? "Black" : "White");
                    _timer.Stop();
                    _dbPollingTimer.Stop();
                    MessageBox.Show("Игра окончена. Вы сдались.","Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var currentUser = _userService.GetUserById(CurrentUserId);
                    var mainForm = new MainForm(_userService, _gameService, _user);
                    mainForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    _logger.Error($"Ошибка при сдаче игры: {ex.Message}");
                    MessageBox.Show("Произошла ошибка при сдаче игры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnendmove_Click(object sender, EventArgs e)
        {
            _logger.Debug("Ход завершён вручную");
            var isPlayersTurn = _gameService.IsPlayersTurn(_gameId, CurrentUserId);
            if (!isPlayersTurn)
            {
                MessageBox.Show("Сейчас не ваш ход", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _gameService.SaveMove(_gameId, CurrentUserId, "-1,-1", "-1,-1");
            EndCurrentTurn();
        }
        private void checkersboardpnl_Paint(object sender, PaintEventArgs e)//рисуем  доску
        {
            var panel = sender as Panel;
            if (panel == null) return;
            var cellSize = panel.Width / 8; 
            for (var row = 0; row < 8; row++)//рисуем все клетки доски
            {
                for (var col = 0; col < 8; col++)
                {
                    var rect = new Rectangle(col * cellSize, row * cellSize, cellSize, cellSize);
                    Brush backgroundBrush = (row % 2 == col % 2) ? Brushes.SaddleBrown : Brushes.Beige;
                    e.Graphics.FillRectangle(backgroundBrush, rect);

                    // подсветка доступных ходов 
                    if (_availableMoves != null && _availableMoves.Contains((col, row)))
                    {
                        e.Graphics.FillRectangle(Brushes.LightGreen, rect);
                        using (Pen greenPen = new Pen(Color.Green, 3))
                        {
                            e.Graphics.DrawRectangle(greenPen, rect);
                        }
                    }
                    var checker = _board.Squares[col, row];
                    if (checker != null)
                    {
                        Color checkerColor = checker.IsWhite ? Color.White : Color.Black;
                        using (Brush checkerBrush = new SolidBrush(checkerColor))
                        {
                            e.Graphics.FillEllipse(checkerBrush, col * cellSize + 5, row * cellSize + 5, cellSize - 10, cellSize - 10);
                        }
                    }
                    if (checker != null && checker.IsKing)//корона для дамки
                    {
                        Color crownColor = checker.IsWhite ? Color.Gold : Color.Silver;
                        using (Brush crownBrush = new SolidBrush(crownColor))
                        {
                            int crownSize = cellSize / 4;
                            int crownX = col * cellSize + cellSize / 2 - crownSize / 2;
                            int crownY = row * cellSize + cellSize / 2 - crownSize / 2;
                            e.Graphics.FillEllipse(crownBrush, crownX, crownY, crownSize, crownSize);
                        }
                    }
                }
            }
        }
        private void сheckersboardpnl_MouseClick(object sender, MouseEventArgs e)//кликаем на шашку
        {
            var squareSize = checkersboardpnl.Width / 8;
            var x = e.X / squareSize;
            var y = e.Y / squareSize;
            if (x < 0 || x >= 8 || y < 0 || y >= 8)
            {
                _logger.Debug($"Клик вне доски: ({x},{y})");
                return;
            }
            var isPlayersTurn = _gameService.IsPlayersTurn(_gameId, CurrentUserId);
            if (!isPlayersTurn)
            {
                _logger.Warn($"Игрок ID: {CurrentUserId} попытался сделать ход вне своей очереди");
                MessageBox.Show("Сейчас не ваш ход", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var checker = _board.Squares[x, y];
            if (_selectedChecker == null)//если шашка не выбрана
            {
                if (checker != null && checker.IsWhite == _currentPlayerIsWhite) 
                {
                    _logger.Debug($"Игрок выбрал шашку на позиции ({x},{y})");
                    _selectedChecker = checker;
                    _availableMoves = _board.GetAvailableMoves(checker);
                    checkersboardpnl.Invalidate();
                }
            }
            else//если выбрана
            {
                if (_availableMoves.Contains((x, y)))
                {
                    _logger.Debug($"Игрок делает ход из ({_selectedChecker.X},{_selectedChecker.Y}) в ({x},{y})");
                    var originalX = _selectedChecker.X;
                    var originalY = _selectedChecker.Y;
                    if (_board.MoveChecker(_selectedChecker, x, y))
                    {
                        if ((_selectedChecker.IsWhite && y == 0) || (!_selectedChecker.IsWhite && y == 7))
                        {
                            _selectedChecker.IsKing = true;//превращение в дамку
                            _logger.Info($"Шашка стала дамкой на позиции ({x},{y})");
                        }
                        _gameService.SaveMove(_gameId, CurrentUserId, $"{originalX},{originalY}", $"{x},{y}");
                        _logger.Info($"Ход сохранён: ({originalX},{originalY}), ({x},{y})");
                        _selectedChecker = null;
                        _availableMoves.Clear();
                        EndCurrentTurn();
                    }
                }
                else if (checker != null && checker.IsWhite == _currentPlayerIsWhite)
                {
                    _logger.Debug($"Выбрана новая шашка: ({x},{y})");
                    _selectedChecker = checker;
                    _availableMoves = _board.GetAvailableMoves(checker);
                    checkersboardpnl.Invalidate();
                }
                else
                {
                    _logger.Debug($"Отмена выбора шашки");
                    _selectedChecker = null;
                    _availableMoves.Clear();
                    checkersboardpnl.Invalidate();
                }
            }
            checkersboardpnl.Invalidate();
        }
        private void UpdatePlayerInfo()
        {
            if (_currentPlayerIsWhite)
            {
                lblstatus1.Text = LanguageManager.GetString("TextStatus1");
                lblstatus1.ForeColor = Color.LightGreen;
                lblstatus2.Text = LanguageManager.GetString("TextStatus2");
                lblstatus2.ForeColor = Color.Black;
            }
            else
            {
                lblstatus1.Text = LanguageManager.GetString("TextStatus2");
                lblstatus1.ForeColor = Color.Black;
                lblstatus2.Text = LanguageManager.GetString("TextStatus1");
                lblstatus2.ForeColor = Color.LightGreen;
            }
        }
        private void MoveTimer(object? sender, EventArgs e)
        {
            var isPlayersTurn = _gameService.IsPlayersTurn(_gameId, CurrentUserId);
            if (!isPlayersTurn)
            {
                _timer.Stop();
                _timeLeft = 30;
                lbltimer.Text = $"Время: 00:30";
                _logger.Debug("Таймер остановлен, тк ход не этого игрока");
                return;
            }
            _timeLeft--;
            lbltimer.Text = $"Время: 00:{_timeLeft:D2}";
            if (_timeLeft <= 0)
            {
                _logger.Warn("Время истекло, ход передан противнику");
                EndCurrentTurn();
            }
        }
        public void EndCurrentTurn()
        {
            _logger.Debug("Завершение хода");
            _selectedChecker = null;
            _availableMoves.Clear();
            SyncCurrentPlayerTurn();
            _timeLeft = 30;
            lbltimer.Text = $"Время: 00:{_timeLeft:D2}";
            UpdatePlayerInfo();
            checkersboardpnl.Invalidate();
            CheckAndStartMoveTimer();
        }
        private void LoadGameFromDatabase()
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            if (game == null) 
                return;
            foreach (var move in game.Moves)
            {
                ApplySingleMove(move);
            }
            _lastMoveCount = game.Moves.Count;
            checkersboardpnl.Invalidate();
            _logger.Debug($"Доска обновлена из БД. Новых ходов: {game.Moves.Count - _lastMoveCount}");
        }
        private void RefreshBoardIfUpdated(object? sender, EventArgs e)
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            if (game == null)
            {
                _logger.Error("Игра не найдена");
                return;
            }
            if (game.Status == "Finished")
            { 
                string message = "Игра окончена";
                if (!string.IsNullOrEmpty(game.Winner))
                {
                    if (game.WhitePlayerId == CurrentUserId || (game.BlackPlayerId.HasValue && game.BlackPlayerId.Value == CurrentUserId))
                    {
                        bool isWinner = game.WhitePlayerId == CurrentUserId && game.Winner == "White" ||
                                        game.BlackPlayerId.HasValue && game.BlackPlayerId.Value == CurrentUserId && game.Winner == "Black";

                        message = isWinner ? "Вы победили!" : "Противник сдался";
                    }
                    else
                    {
                    message = "Противник сдался";
                    }
                    _timer.Stop();
                    _dbPollingTimer.Stop();
                    MessageBox.Show(message, "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var mainForm = new MainForm(_userService, _gameService, _user);
                    mainForm.Show();
                    this.Close();
                    return;
                }
            }
            if (game.Moves.Count > _lastMoveCount)
            {
                _logger.Info($"Обнаружены новые ходы. Количество: {game.Moves.Count - _lastMoveCount}");
                foreach (var move in game.Moves.Skip(_lastMoveCount))
                {
                    ApplySingleMove(move);
                }
                _lastMoveCount = game.Moves.Count;
                checkersboardpnl.Invalidate();
            }
            SyncCurrentPlayerTurn();
            CheckAndStartMoveTimer();
        }
        public void CheckAndStartMoveTimer()
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            if (game == null)
            {
                _logger.Error("Игра не найдена");
                return;
            }
            if (game.Status != "Active")
            {
                _timer.Stop();
                _logger.Debug("Таймер остановлен, игра неактивна");
                return;
            }
            if (!game.BlackPlayerId.HasValue)
            {
                _timer.Stop();
                _logger.Debug("Таймер остановлен - ждем второго игрока");
                return;
            }
            bool isPlayersTurn = false;
            if (game.WhitePlayerId == CurrentUserId && game.Turn == "White")
                isPlayersTurn = true;
            else if (game.BlackPlayerId == CurrentUserId && game.Turn == "Black")
                isPlayersTurn = true;
            if (isPlayersTurn)
            {
                if (!_timer.Enabled) // для избежания лишних запусков
                {
                    _timeLeft = 30;
                    lbltimer.Text = $"Время: 00:30";
                    _timer.Start();
                    _logger.Info("Таймер хода запущен, оба игрока подключены");
                }
            }
            else
            {
                _timer.Stop();
                _logger.Debug("Таймер остановлен, ход не этого игрока");
            }
            SyncCurrentPlayerTurn();
            checkersboardpnl.Invalidate();
        }
        private (int, int) ParsePosition(string position)
        {
            var parts = position.Split(',');
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }
        private void ApplySingleMove(Move move)
        {
            if (move.FromPosition == "-1,-1" && move.ToPosition == "-1,-1")
            {
                return;
            }
            var from = ParsePosition(move.FromPosition);
            var to = ParsePosition(move.ToPosition);
            var checker = _board.Squares[from.Item1, from.Item2];
            if (checker != null)
            {
                if (_board.MoveChecker(checker, to.Item1, to.Item2))
                {
                    if ((checker.IsWhite && to.Item2 == 0) || (!checker.IsWhite && to.Item2 == 7))
                    {
                        checker.IsKing = true;
                        _logger.Info($"Шашка стала дамкой на позиции ({to.Item1},{to.Item2})");
                    }
                }
            }
        }
        private void SyncCurrentPlayerTurn()
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            if (game == null) 
                return;
            bool newTurn = game.Turn == "White";
            _currentPlayerIsWhite = newTurn;
            UpdatePlayerInfo();
            checkersboardpnl.Invalidate();
            _logger.Debug($"Текущая очередь хода: {(newTurn ? "Белые" : "Чёрные")}");
        }
    }
}



