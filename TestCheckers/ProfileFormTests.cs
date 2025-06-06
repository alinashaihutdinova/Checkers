using Checkers.Core.Services;
using Checkers.Forms;
using Moq;
using Checkers.Core.Entities;

namespace TestCheckers
{
    public class ProfileFormTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly User testUser;

        public ProfileFormTests()
        {
            userServiceMock = new Mock<IUserService>();
            testUser = new User { Id = Guid.NewGuid(), Login = "TestUser", GamesPlayed = 5, Wins = 3, Losses = 2 };

            // Настройка мока для GetUserById
            userServiceMock.Setup(us => us.GetUserById(testUser.Id)).Returns(testUser);

            // Настройка мока для GetAllUsersSortedByRating
            var users = new List<User> { testUser };
            userServiceMock.Setup(us => us.GetAllUsersSortedByRating()).Returns(users);
        }

        [Fact]
        public void Constructor_SetsLabels_WhenUserIsProvided()
        {
            var form = new ProfileForm(userServiceMock.Object, testUser);

            Assert.Equal("TestUser", form.LblLogin.Text);
            Assert.Equal("Игры: 5", form.LblGames.Text);
            Assert.Equal("Победы: 3", form.LblWins.Text);
            Assert.Equal("Поражения: 2", form.LblLosses.Text);
        }

        [Fact]
        public void btnChangeLanguage_Click_TogglesLanguage()
        {
            var form = new ProfileForm(userServiceMock.Object, testUser);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");

            form.btnChangeLanguage_Click(null, EventArgs.Empty);

            Assert.Equal("en", Thread.CurrentThread.CurrentUICulture.Name);
            form.btnChangeLanguage_Click(null, EventArgs.Empty);
            Assert.Equal("ru", Thread.CurrentThread.CurrentUICulture.Name);
        }

        [Fact]
        public void btnback_Click_HidesForm()
        {
            var form = new ProfileForm(userServiceMock.Object, testUser);
            form.Show();

            form.btnback_Click(null, EventArgs.Empty);

            Assert.False(form.Visible);
        }
    }
}
