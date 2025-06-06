using Castle.Windsor;
using Checkers.Core.Services;
using Checkers;
using Moq;

namespace TestCheckers
{
    public class EntranceFormTests
    {
        private readonly Mock<IWindsorContainer> containerMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IGameService> gameServiceMock;

        public EntranceFormTests()
        {
            userServiceMock = new Mock<IUserService>();
            gameServiceMock = new Mock<IGameService>();
            containerMock = new Mock<IWindsorContainer>();
            containerMock.Setup(c => c.Resolve<IUserService>()).Returns(userServiceMock.Object);
            containerMock.Setup(c => c.Resolve<IGameService>()).Returns(gameServiceMock.Object);
        }

        [Fact]
        public void btnTogglePassword_Click_TogglesPasswordVisibility()
        {
            var form = new EntranceForm(containerMock.Object);
            form.TxtPassword.Text = "Password123";
            form.TxtPassword.UseSystemPasswordChar = true;

            form.btnTogglePassword_Click(null, EventArgs.Empty);

            Assert.False(form.TxtPassword.UseSystemPasswordChar);
            form.btnTogglePassword_Click(null, EventArgs.Empty);
            Assert.True(form.TxtPassword.UseSystemPasswordChar);
        }

        [Fact]
        public void btnRegister_Click_HidesForm()
        {
            var form = new EntranceForm(containerMock.Object);
            form.Show();

            form.btnRegister_Click(null, EventArgs.Empty);

            Assert.False(form.Visible);
        }
    }
}
