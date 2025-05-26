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
        }
    }
}
