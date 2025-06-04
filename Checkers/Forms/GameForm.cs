using Checkers.Classes;
using Checkers.Core.Services;
using NLog;

namespace Checkers.Forms
{
    /// <summary>
    /// форма для игры в шашки
    /// </summary>
    public partial class GameForm : Form
    {
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
        public GameForm(IUserService userService, IGameService gameService, Guid gameId, bool isWhite, Guid currentUserId)
        {
            InitializeComponent();
            _userService = userService;
            _gameService = gameService;
            _gameId = gameId;
            _isWhite = isWhite;
            CurrentUserId = currentUserId;
            InitializeBoard();
            InitializeTimers();
            LoadAndStartGame();
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
            _timer.Start();

            _dbPollingTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            _dbPollingTimer.Tick += RefreshBoardIfUpdated;
            _dbPollingTimer.Start();
            _logger.Debug("Таймеры запущены");
        }
        private void LoadAndStartGame()
        {
            UpdatePlayerInfo();
            LoadGameFromDatabase();
            var game = _gameService.GetGameWithMoves(_gameId);
            _lastMoveCount = game.Moves.Count;
            _logger.Info($"Игра загружена. Количество ходов: {_lastMoveCount}");
        }
        private void btngiveup_Click(object sender, EventArgs e)
        {
            _logger.Warn($"Пользователь сдался. Игра ID: {_gameId}");
            var result = MessageBox.Show("Вы действительно хотите сдаться?","Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _gameService.EndGame(_gameId, _isWhite ? "Black" : "White");
                _timer.Stop();
                MessageBox.Show("Игра окончена. Вы сдались.","Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var mainForm = new MainForm(_userService, _gameService, _user);
                mainForm.Show();
                this.Close();
            }
        }
        private void btnendmove_Click(object sender, EventArgs e)
        {
            _logger.Debug("Ход завершён вручную");
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
            if (!_gameService.IsPlayersTurn(_gameId, CurrentUserId))
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
                    if (_board.MoveChecker(_selectedChecker, x, y))
                    {
                        if ((_selectedChecker.IsWhite && y == 0) || (!_selectedChecker.IsWhite && y == 7))
                        {
                            _selectedChecker.IsKing = true;//превращение в дамку
                            _logger.Info($"Шашка стала дамкой на позиции ({x},{y})");
                        }
                        _gameService.SaveMove(_gameId, CurrentUserId, $"{_selectedChecker.X},{_selectedChecker.Y}", $"{x},{y}");
                        _logger.Info($"Ход сохранён: ({_selectedChecker.X},{_selectedChecker.Y}), ({x},{y})");
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
        private void MoveTimer(object sender, EventArgs e)
        {
            _timeLeft--;
            lbltimer.Text = $"Время: 00:{_timeLeft:D2}";

            if (_timeLeft <= 0)
            {
                _logger.Warn("Время истекло. Ход передан противнику");
                EndCurrentTurn();
            }
        }
        private void EndCurrentTurn()
        {
            _logger.Debug("Завершение хода");
            _selectedChecker = null;
            _availableMoves.Clear();
            _currentPlayerIsWhite = !_currentPlayerIsWhite;
            _timeLeft = 30; 
            UpdatePlayerInfo();
            checkersboardpnl.Invalidate();
        }
        private void LoadGameFromDatabase()
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            _board = new Board();
            _board.InitializeBoard();
            foreach (var move in game.Moves.OrderBy(m => m.MoveNumber))
            {
                var from = move.FromPosition.Split(',').Select(int.Parse).ToArray();
                var to = move.ToPosition.Split(',').Select(int.Parse).ToArray();

                var checker = _board.Squares[from[0], from[1]];
                if (checker != null)
                {
                    _board.MoveChecker(checker, to[0], to[1]);
                }
            }
            checkersboardpnl.Invalidate();
            _logger.Debug($"Доска обновлена из БД. Новых ходов: {game.Moves.Count - _lastMoveCount}");
        }
        private void RefreshBoardIfUpdated(object sender, EventArgs e)
        {
            var game = _gameService.GetGameWithMoves(_gameId);
            if (game.Moves.Count > _lastMoveCount)
            {
                _logger.Info($"Обнаружены новые ходы. Количество: {game.Moves.Count - _lastMoveCount}");
                LoadGameFromDatabase(); // перезагружаем доску
                _lastMoveCount = game.Moves.Count;
            }
        }
    }
}



