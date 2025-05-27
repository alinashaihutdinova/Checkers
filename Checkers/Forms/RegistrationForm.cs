using Checkers.Core.Services;
using Checkers.Core.Entities;
using Castle.Windsor;

namespace Checkers.Forms
{
    /// <summary>
    /// форма регистрации нового пользователя 
    /// </summary>
    public partial class RegistrationForm : Form
    {
        private readonly IWindsorContainer _container;
        private readonly IUserService _userService;
        private bool passwordVisible = false;
        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="userService">Сервис аутентификации</param>
        public RegistrationForm(IWindsorContainer container)
        {
            InitializeComponent();
            _container = container;
            _userService = _container.Resolve<IUserService>();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;
            var repeatpassword = txtRepeatpassword.Text;

            if (string.IsNullOrWhiteSpace(login) || login == "Логин")
            {
                MessageBox.Show("Пожалуйста, введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || password == "Пароль")
            {
                MessageBox.Show("Пожалуйста, введите пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password != repeatpassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    PasswordHash = _userService.HashPassword(password)
                };
                _userService.RegisterUser(user);
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                var mainForm = new MainForm(_userService); // Передаем контейнер в форму входа
                mainForm.Show();
            }
            catch (Exception ex)
            {
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
        private void pctrBoxProfile_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
