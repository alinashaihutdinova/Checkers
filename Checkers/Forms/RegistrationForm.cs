

namespace Checkers.Forms
{
    /// <summary>
    /// Форма регистрации нового пользователя 
    /// </summary>
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        public RegistrationForm(IServiceProvider serviceProvider) : this()
        {
     
        }
    }
}
