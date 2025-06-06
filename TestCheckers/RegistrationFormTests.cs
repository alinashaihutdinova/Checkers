using Xunit;
using Moq;
using Checkers.Forms;
using Checkers.Core.Services;
using Checkers.Core.Entities;
using Castle.Windsor;

namespace TestCheckers
{
    public class RegistrationFormTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IGameService> _gameServiceMock;
        private readonly Mock<IWindsorContainer> _containerMock;

        public RegistrationFormTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _gameServiceMock = new Mock<IGameService>();
            _containerMock = new Mock<IWindsorContainer>(); //настроен для возврата мок-объектов _userServiceMock и _gameServiceMock
            _containerMock.Setup(c => c.Resolve<IUserService>()).Returns(_userServiceMock.Object);
            _containerMock.Setup(c => c.Resolve<IGameService>()).Returns(_gameServiceMock.Object);
        }

        [Fact]
        public void BtnRegister_Click_WhenDataIsCorrect()
        {
            _userServiceMock.Setup(us => us.HashPassword("Password123")).Returns("hashedPassword");
            _userServiceMock.Setup(us => us.RegisterUser(It.IsAny<User>()));
            var form = new RegistrationForm(_containerMock.Object);
            form.TxtLogin.Text = "NewUser";
            form.TxtPassword.Text = "Password123";
            form.TxtRepeatpassword.Text = "Password123";

            form.BtnRegister_Click(null, EventArgs.Empty);

            _userServiceMock.Verify(us => us.RegisterUser(It.Is<User>(u => u.Login == "NewUser" && u.PasswordHash == "hashedPassword")), Times.Once());
        }

        [Fact]
        public void BtnRegister_Click_ThrowsError_WhenPasswordsDoNotMatch()
        {
            var form = new RegistrationForm(_containerMock.Object);
            form.TxtLogin.Text = "NewUser";
            form.TxtPassword.Text = "Password123";
            form.TxtRepeatpassword.Text = "DifferentPassword";

            form.BtnRegister_Click(null, EventArgs.Empty);

            _userServiceMock.Verify(us => us.RegisterUser(It.IsAny<User>()), Times.Never());
        }

        [Fact]
        public void btnTogglePassword1_Click_TogglesPasswordVisibility()
        {
            var form = new RegistrationForm(_containerMock.Object);
            form.TxtPassword.Text = "Password123";
            form.TxtPassword.UseSystemPasswordChar = true;

            form.btnTogglePassword1_Click(null, EventArgs.Empty);

            Assert.False(form.TxtPassword.UseSystemPasswordChar);
            form.btnTogglePassword1_Click(null, EventArgs.Empty);
            Assert.True(form.TxtPassword.UseSystemPasswordChar);
        }
    }
}