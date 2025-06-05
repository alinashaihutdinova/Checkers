using Checkers.Forms;
using Checkers.Core.Services;
using Castle.Windsor;
using NLog;
using Checkers.Classes;
namespace Checkers
{
    /// <summary>
    /// ����� ��� ����� ������������ � �������
    /// </summary>
    public partial class EntranceForm : Form
    {
        private bool passwordVisible = false;
        private readonly IWindsorContainer _container;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// ����������� ������ � ��������� ����������� 
        /// </summary>
        public EntranceForm(IWindsorContainer container)
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
            btnLogin.Text = LanguageManager.GetString("ButtonLogIn");
            txtLogin.Text = LanguageManager.GetString("TextLogin");
            txtPassword.Text = LanguageManager.GetString("TextPassword");
            btnRegister.Text = LanguageManager.GetString("ButtonRegister");
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || login == "�����")
            {
                _logger.Debug("������� �����: ����� �� ������");
                MessageBox.Show("����������, ������� �����", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password == "������")
            {
                _logger.Debug("������� �����: ������ �� ������");
                MessageBox.Show("����������, ������� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var user = _userService.Authenticate(login, password);
            if (user == null)
            {
                _logger.Error("�������� ����� ��� ������");
                MessageBox.Show("�������� ����� ��� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.Info($"������������ �����: {user.Login} (ID: {user.Id})");
            var mainForm = new MainForm(_userService, _gameService, user);
            mainForm.Show();
            this.Hide();
        }
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = false; 
            }
            else
            {
                if (txtPassword.Text != "������")
                {
                    txtPassword.UseSystemPasswordChar = true; // �������� ������
                }
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            _logger.Debug("������� � �����������");
            var registrationForm = new RegistrationForm(_container);
            registrationForm.Show();
            this.Hide();
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "������")
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
                txtPassword.Text = "������";
                txtPassword.ForeColor = Color.Gray;
            }
        }
    }
}
