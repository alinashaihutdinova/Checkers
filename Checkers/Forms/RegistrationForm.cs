using Checkers.Core.Services;
using Checkers.Core.Entities;
using Castle.Windsor;
using NLog;
using Checkers.Classes;

namespace Checkers.Forms
{
    /// <summary>
    /// форма регистрации нового пользователя 
    /// </summary>
    public partial class RegistrationForm : Form
    {
        private readonly IWindsorContainer _container;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private bool passwordVisible = false;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// конструктор класса
        /// </summary>
        public RegistrationForm(IWindsorContainer container)
        {
            InitializeComponent();
            _container = container;
            _userService = _container.Resolve<IUserService>();
            _gameService = _container.Resolve<IGameService>();
            LanguageManager.OnLanguageChanged += UpdateLanguage;
            UpdateLanguage();
        }
        private void UpdateLanguage()
        {
            lblTitle.Text = LanguageManager.GetString("Title");
            btnRegister.Text = LanguageManager.GetString("ButtonRegister");
            txtLogin.Text = LanguageManager.GetString("TextLogin");
            txtPassword.Text = LanguageManager.GetString("TextPassword");
            txtRepeatpassword.Text = LanguageManager.GetString("TextRepeatPassword");
            btnUploudphoto.Text = LanguageManager.GetString("ButtonLoadPhoto");
            btnBack.Text = LanguageManager.GetString("ButtonBack");
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;
            var repeatpassword = txtRepeatpassword.Text;

            if (string.IsNullOrWhiteSpace(login) || login == "Логин")
            {
                _logger.Debug("Регистрация: логин не указан");
                MessageBox.Show("Пожалуйста, введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || password == "Пароль")
            {
                _logger.Debug("Регистрация: пароль не указан");
                MessageBox.Show("Пожалуйста, введите пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password != repeatpassword)
            {
                _logger.Debug("Регистрация: Пароли не совпадают");
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _logger.Debug($"Регистрация: Создание пользователя {login}");
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    PasswordHash = _userService.HashPassword(password)
                };
                _userService.RegisterUser(user);
                _logger.Info($"Пользователь зарегистрирован: {login} (ID: {user.Id})");
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                var mainForm = new MainForm(_userService, _gameService, user); 
                mainForm.Show();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка регистрации: {ex.Message}");
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTogglePassword1_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                if (txtPassword.Text != "Пароль")
                {
                    txtPassword.UseSystemPasswordChar = true; // скрываем пароль
                }
            }
        }
        private void btnTogglePassword2_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                if (txtPassword.Text != "Подтверждение пароля")
                {
                    txtPassword.UseSystemPasswordChar = true; // скрываем пароль
                }
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            _logger.Debug("Возврат на экран входа");
            Hide();
            var entranceForm = new EntranceForm(_container);
            entranceForm.Show();
        }
        private void BtnUploudPhoto_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pctrBoxProfile.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }
    }
}
