
namespace Checkers.Classes
{
    /// <summary>
    /// класс для представления шашки на доске
    /// </summary>
    public class Checker
    {
        // <summary>
        /// айди шашки
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// является ли шашка дамкой
        /// </summary>
        public bool IsKing { get; set; }
        /// <summary>
        /// определяет цвет шашки, true - белая, false - черная
        /// </summary>
        public bool IsWhite { get; set; } 
        /// <summary>
        /// позиция по горизонтали 
        /// </summary>
        public int X { get; set; } 
        /// <summary>
        /// позиция по вертикали 
        /// </summary>
        public int Y { get; set; } 
    }
}
