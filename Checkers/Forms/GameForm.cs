using Checkers.Classes;
using Checkers.Core.Services;

namespace Checkers.Forms
{
    /// <summary>
    /// форма для игры в шашки
    /// </summary>
    public partial class GameForm : Form
    {
        private readonly IUserService _userService;
        private Board _board;
        private Checker? _selectedChecker;
        private List<(int, int)> _availableMoves = new List<(int, int)>();
        private int _timeLeft = 30;
        private System.Windows.Forms.Timer _timer;
        private bool _currentPlayerIsWhite = true;
        /// <summary>
        /// конструктор формы
        /// </summary>
        public GameForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            _board = new Board();
            _board.InitializeBoard();
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // 1 секунда
            _timer.Tick += MoveTimer;
            _timer.Start();
            UpdatePlayerInfo();
        }

        private void btngiveup_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите сдаться?","Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _timer.Stop();
                MessageBox.Show("Игра окончена. Вы сдались.","Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
                this.Close();
            }
        }

        private void btnendmove_Click(object sender, EventArgs e)
        {
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
                    using (var brush = new SolidBrush(row % 2 == col % 2 ? Color.SaddleBrown : Color.Beige))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    // подсветка доступных ходов 
                    if (_availableMoves != null && _availableMoves.Contains((col, row)))
                    {
                        using (var highlightBrush = new SolidBrush(Color.LightGreen)) 
                        {
                            e.Graphics.FillRectangle(highlightBrush, rect);
                        }
                        using (var pen = new Pen(Color.Green, 3))
                        {
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                    }
                    var checker = _board.Squares[col, row];
                    if (checker != null)
                    {
                        using (var checkerBrush = new SolidBrush(checker.IsWhite ? Color.White : Color.Black))//рисуем шашку
                        {
                            e.Graphics.FillEllipse(checkerBrush, col * cellSize + 5, row * cellSize + 5, cellSize - 10, cellSize - 10);
                        }
                    }
                    if (checker != null && checker.IsKing)//корона для дамки
                    {
                        using (var crownBrush = new SolidBrush(checker.IsWhite ? Color.Gold : Color.Silver))
                        {
                            var crownSize = cellSize / 4;
                            var crownX = col * cellSize + cellSize / 2 - crownSize / 2;
                            var crownY = row * cellSize + cellSize / 2 - crownSize / 2;

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
                return;
            var checker = _board.Squares[x, y];
            if (_selectedChecker == null)
            {
                if (checker != null && checker.IsWhite == _currentPlayerIsWhite) 
                {
                    _selectedChecker = checker;
                    _availableMoves = _board.GetAvailableMoves(checker);
                    checkersboardpnl.Invalidate();
                }
            }
            else
            {
                if (_availableMoves.Contains((x, y)))
                {
                    if (_board.MoveChecker(_selectedChecker, x, y))
                    {
                        if ((_selectedChecker.IsWhite && y == 0) || (!_selectedChecker.IsWhite && y == 7))
                        {
                            _selectedChecker.IsKing = true;
                        }
                        _selectedChecker = null;
                        _availableMoves.Clear();
                        EndCurrentTurn();
                    }
                }
                else if (checker != null && checker.IsWhite == _currentPlayerIsWhite)
                {
                    _selectedChecker = checker;
                    _availableMoves = _board.GetAvailableMoves(checker);
                    checkersboardpnl.Invalidate();
                }
                else
                {
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
                lblstatus1.Text = "Ваш ход";
                lblstatus1.ForeColor = Color.LightGreen;
                lblstatus2.Text = "Ожидание";
                lblstatus2.ForeColor = Color.Black;
            }
            else
            {
                lblstatus1.Text = "Ожидание";
                lblstatus1.ForeColor = Color.Black;
                lblstatus2.Text = "Ваш ход";
                lblstatus2.ForeColor = Color.LightGreen;
            }
        }
        private void MoveTimer(object sender, EventArgs e)
        {
            _timeLeft--;
            lbltimer.Text = $"Время: 00:{_timeLeft:D2}";

            if (_timeLeft <= 0)
            {
                EndCurrentTurn();
            }
        }
        private void EndCurrentTurn()
        {
            _selectedChecker = null;
            _availableMoves.Clear();
            _currentPlayerIsWhite = !_currentPlayerIsWhite;
            _timeLeft = 30; 
            UpdatePlayerInfo();
            checkersboardpnl.Invalidate();
        }
    }
}



