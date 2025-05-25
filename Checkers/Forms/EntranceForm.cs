using Checkers.Forms;
using Checkers.Core.Services;
using Castle.Windsor;
namespace Checkers
{
    /// <summary>
    /// ����� ��� ����� ������������ � �������
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
            var mainForm = _container.Resolve<MainForm>();
            mainForm.Show();
            this.Hide();
        }
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            if (passwordVisible)
            {
                txtPassword.UseSystemPasswordChar = false; //��������� ���������� ��������
                btnTogglePassword.Text = "??"; 
            }
            else
            {
                if (txtPassword.Text == "������")
                {
                    txtPassword.UseSystemPasswordChar = false; //��������� ����� �������
                }
                else
                {
                    txtPassword.UseSystemPasswordChar = true; //�������� ���������� 
                }
                btnTogglePassword.Text = "??"; //���� ���, �� ���������� ����� �������� ����
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
