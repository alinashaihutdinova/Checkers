using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Checkers.Data; 
using Checkers.Core.Entities;
using Checkers.Forms;
namespace Checkers
{
    /// <summary>
    /// Форма для входа пользователя в систему
    /// </summary>
    public partial class EntranceForm : Form
    {
        private bool passwordVisible = false;
        public EntranceForm()
        {
            InitializeComponent();
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
            using (var context = new CheckersDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string hashedPassword = HashPassword(password);
                if (user.PasswordHash != hashedPassword)
                {
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            MessageBox.Show("Вход выполнен успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            var mainForm = new MainForm();
            mainForm.Show();
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
            this.Hide();
            var registrationForm = new RegistrationForm();
            registrationForm.Show();
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
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
    }
}
