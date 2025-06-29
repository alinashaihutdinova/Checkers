﻿using Checkers.Core.Services;
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
        public TextBox TxtLogin { get => txtLogin; set => txtLogin = value; }
        public TextBox TxtPassword { get => txtPassword; set => txtPassword = value; }
        public TextBox TxtRepeatpassword { get => txtRepeatpassword; set => txtRepeatpassword = value; }

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
        public void BtnRegister_Click(object sender, EventArgs e)
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
        public void btnTogglePassword1_Click(object sender, EventArgs e)
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
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Пароль")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Пароль";
                txtPassword.ForeColor = Color.Gray;
            }
        }
        private void btnTogglePassword2_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtRepeatpassword.UseSystemPasswordChar = false;
            }
            else
            {
                if (txtRepeatpassword.Text != "Подтверждение пароля")
                {
                    txtRepeatpassword.UseSystemPasswordChar = true; // скрываем пароль
                }
            }
        }
        private void txtRepeatpassword_Enter(object sender, EventArgs e)
        {
            if (txtRepeatpassword.Text == "Подтверждение пароля")
            {
                txtRepeatpassword.Text = "";
                txtRepeatpassword.ForeColor = Color.Black;
                txtRepeatpassword.UseSystemPasswordChar = true;
            }
        }
        private void txtRepeatpassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtRepeatpassword.UseSystemPasswordChar = false;
                txtRepeatpassword.Text = "Подтверждение пароля";
                txtRepeatpassword.ForeColor = Color.Gray;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            _logger.Debug("Возвращение на форму входа");
            Hide();
            var entranceForm = new EntranceForm(_container);
            entranceForm.Show();
        }
        public void BtnUploudPhoto_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pctrBoxProfile.Image = Image.FromFile(openFileDialog.FileName);
                    btnUploudphoto.Visible = false;
                }
            }
        }
    }
}
