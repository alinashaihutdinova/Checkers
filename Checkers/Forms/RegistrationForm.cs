
using Checkers.Core.Services;

namespace Checkers.Forms
{
    /// <summary>
    /// форма регистрации нового пользователя 
    /// </summary>
    public partial class RegistrationForm : Form
    {
        private readonly IUserService _userService;
        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="userService">сервис аутентификации и регистрации пользователей</param>
        public RegistrationForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }
    }
}
