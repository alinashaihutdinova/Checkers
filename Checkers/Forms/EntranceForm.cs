using Checkers.Forms;
using Checkers.Core.Services;
using Castle.Windsor;
namespace Checkers
{
    /// <summary>
    /// Форма для входа пользователя в систему
    /// </summary>
    public partial class EntranceForm : Form
    {
        private bool passwordVisible = false;
        private readonly IUserService _userService;
        private readonly IWindsorContainer _container;
        public EntranceForm(IUserService userService, IWindsorContainer container)
        {
            InitializeComponent();
            _userService = userService;
            _container = container;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

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

            var user = _userService.Authenticate(login, password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var mainForm = _container.Resolve<MainForm>();
            mainForm.Show();
            this.Hide();
        }
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = false; //отключаем маскировку символов
                btnTogglePassword.Text = "??"; 
            }
            else
            {
                if (txtPassword.Text == "Пароль")
                {
                    txtPassword.UseSystemPasswordChar = false; //оставляем текст видимым
                }
                else
                {
                    txtPassword.UseSystemPasswordChar = true; //включаем маскировку 
                }
                btnTogglePassword.Text = "??"; //пока так, не получается найти закрытый глаз
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registrationForm = _container.Resolve<RegistrationForm>();
            registrationForm.Show();
            this.Hide();
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
    }
}
