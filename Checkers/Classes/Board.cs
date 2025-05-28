
namespace Checkers.Classes
{
    /// <summary>
    /// класс для представления игровой доски 
    /// </summary>
    public class Board
    {
        /// <summary>
        /// двумерный массив клеток 
        /// </summary>
        public Checker?[,] Squares { get; set; } = new Checker[8, 8];
        /// <summary>
        /// расставляем шашки в начальные позиции
        /// </summary>
        public void InitializeBoard()
        {
            for (var i = 0; i < 8; i++)//проходимся по всем столбцам
            {
                if (i % 2 == 0)//четные
                {
                    Squares[i, 0] = new Checker { IsWhite = false, X = i, Y = 0 };
                    Squares[i, 2] = new Checker { IsWhite = false, X = i, Y = 2 };
                    Squares[i, 6] = new Checker { IsWhite = true, X = i, Y = 6 };
                }
                else
                {
                    Squares[i, 1] = new Checker { IsWhite = false, X = i, Y = 1 };
                    Squares[i, 5] = new Checker { IsWhite = true, X = i, Y = 5 };
                    Squares[i, 7] = new Checker { IsWhite = true, X = i, Y = 7 };
                }
            }
        }
        /// <summary>
        /// метод перемещает шашку на новую позицию
        /// </summary> 
        public bool MoveChecker(Checker checker, int targetX, int targetY)
        {
            if (checker == null) 
                return false; //проверка на null
            if (!IsValidMove(checker, targetX, targetY)) 
                return false; //проверка на допустимость хода
            if (checker.IsKing)
            {
                var deltaX = Math.Sign(targetX - checker.X);
                var deltaY = Math.Sign(targetY - checker.Y);
                var currentX = checker.X + deltaX;
                var currentY = checker.Y + deltaY;
                while (currentX != targetX && currentY != targetY)
                {
                    var checkerOnPath = Squares[currentX, currentY];
                    if (checkerOnPath != null && checkerOnPath.IsWhite != checker.IsWhite)
                    {
                        Squares[currentX, currentY] = null; 
                    }
                    currentX += deltaX;
                    currentY += deltaY;
                }
            }
            else
            {
                var direction = checker.IsWhite ? -1 : 1;
                if (Math.Abs(targetX - checker.X) == 2 && targetY == checker.Y + 2 * direction)
                {
                    var middleX = (checker.X + targetX) / 2;
                    var middleY = (checker.Y + targetY) / 2;
                    Squares[middleX, middleY] = null;
                }
            }
            Squares[checker.X, checker.Y] = null; //убираем шашку с текущей позиции
            checker.X = targetX;
            checker.Y = targetY;
            Squares[targetX, targetY] = checker; //ставим шашку на новую позицию
            return true;
        }
        private bool IsValidMove(Checker checker, int targetX, int targetY)//проверяем допустимость хода
        {
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8)
                return false;
            if (Squares[targetX, targetY] != null)
                return false;
            if (Math.Abs(targetX - checker.X) != Math.Abs(targetY - checker.Y))
                return false;

            if (checker.IsKing)
            {
                return IsValidKingMove(checker, targetX, targetY);//для дамки
            }
            else
            {
                var direction = checker.IsWhite ? -1 : 1; //белые движутся вверх, черные вниз
                if (targetY == checker.Y + direction && (targetX == checker.X - 1 || targetX == checker.X + 1))
                {
                    return true; //обычный ход
                }
                if (targetY == checker.Y + 2 * direction)
                {
                    var middleX = (checker.X + targetX) / 2;
                    var middleY = checker.Y + direction;
                    if (Squares[middleX, middleY] != null && Squares[middleX, middleY].IsWhite != checker.IsWhite)
                    {
                        return true; //ход с захватом
                    }
                }
                return false;
            }
        }
        private bool IsValidKingMove(Checker king, int targetX, int targetY)//метод для дамки
        {
            var deltaX = Math.Sign(targetX - king.X);
            var deltaY = Math.Sign(targetY - king.Y);
            var currentX = king.X + deltaX;
            var currentY = king.Y + deltaY;
            var enemyCount = 0;
            while (currentX != targetX && currentY != targetY)
            {
                var checkerOnPath = Squares[currentX, currentY];
                if (checkerOnPath != null)
                {
                    if (checkerOnPath.IsWhite == king.IsWhite)
                        return false; 
                    else
                    {
                        enemyCount++;
                        if (enemyCount > 1)
                            return false; 
                    }
                }
                currentX += deltaX;
                currentY += deltaY;
            }

            return true;
        }
        /// <summary>
        /// возвращает все возможные ходы для шашки
        /// </summary>
        public List<(int, int)> GetAvailableMoves(Checker checker)
        {
            List<(int, int)> availableMoves = new List<(int, int)>();
            if (checker.IsKing)//сначала логика для дамки
            {
                var directions = new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) };

                foreach (var (dx, dy) in directions)
                {
                    for (var distance = 1; distance < 8; distance++)
                    {
                        var targetX = checker.X + dx * distance;
                        var targetY = checker.Y + dy * distance;

                        if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8)
                            break;

                        if (IsValidMove(checker, targetX, targetY))
                        {
                            availableMoves.Add((targetX, targetY));
                        }
                        else
                        {
                            break; 
                        }
                    }
                }
            }
            else//для обычной шашки
            {
                var direction = checker.IsWhite ? -1 : 1; 
                for (var dx = -1; dx <= 1; dx += 2)
                {
                    var targetX = checker.X + dx;
                    var targetY = checker.Y + direction;
                    if (IsValidMove(checker, targetX, targetY))
                    {
                        availableMoves.Add((targetX, targetY));
                    }
                }
                for (var dx = -1; dx <= 1; dx += 2)//проверка на захваты
                {
                    var targetX = checker.X + dx * 2;
                    var targetY = checker.Y + direction * 2;
                    var middleX = checker.X + dx;
                    var middleY = checker.Y + direction;
                    if (IsValidMove(checker, targetX, targetY) && Squares[middleX, middleY] != null && Squares[middleX, middleY].IsWhite != checker.IsWhite)
                    {
                        availableMoves.Add((targetX, targetY));
                    }
                }
            }
            return availableMoves;
        }
    }
}
