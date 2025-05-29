using Checkers.Forms;
using Checkers.Core.Services;
using Castle.Windsor;
using Checkers.Services;
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
        /// <summary>
        /// ����������� ������ � ��������� ����������� 
        /// </summary>
        /// <param name="container">��������� Windsor</param>
        public EntranceForm(IWindsorContainer container)
        {
            InitializeComponent();
            _container = container;
            _userService = _container.Resolve<IUserService>();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || login == "�����")
            {
                MessageBox.Show("����������, ������� �����", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password == "������")
            {
                MessageBox.Show("����������, ������� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var user = _userService.Authenticate(login, password);
            if (user == null)
            {
                MessageBox.Show("�������� ����� ��� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        private void EntranceForm_Load(object sender, EventArgs e)
        {

        }
    }
}
