using Checkers.Core.Entities;
using Checkers.Core.Services;
using Checkers.Forms;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;

namespace TestCheckers
{
    public class MainFormTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IGameService> gameServiceMock;
        private readonly Checkers.Core.Entities.User testUser;
        private readonly List<Checkers.Core.Entities.User> users;
        private readonly Mock<NLog.ILogger> loggerMock;

        public MainFormTests()
        {
            userServiceMock = new Mock<IUserService>();
            gameServiceMock = new Mock<IGameService>();
            loggerMock = new Mock<NLog.ILogger>();
            testUser = new Checkers.Core.Entities.User { Id = Guid.NewGuid(), Login = "TestUser" };

            // Настройка мока для GetAllUsersSortedByRating
            users = new List<Checkers.Core.Entities.User>
            {
                new Checkers.Core.Entities.User { Id = Guid.NewGuid(), Login = "User1", Wins = 5, Losses = 2 },
                new Checkers.Core.Entities.User { Id = Guid.NewGuid(), Login = "User2", Wins = 3, Losses = 4 }
            };
            userServiceMock.Setup(us => us.GetAllUsersSortedByRating()).Returns(users);

            // Настройка мока для GetGameWithMoves
            var game = new Game
            {
                Id = It.IsAny<Guid>(),
                Status = "Active",
                Moves = new List<Move>()
            };
            gameServiceMock.Setup(gs => gs.GetGameWithMoves(It.IsAny<Guid>())).Returns(game);
        }

        [Fact]
        public void BtnPlay_Click_CreatesNewGame()
        {
            var gameId = Guid.NewGuid();
            var game = new Game { Id = gameId, WhitePlayerId = testUser.Id, Status = "Waiting", StartedAt = DateTime.UtcNow, Moves = new List<Move>() };
            gameServiceMock.Setup(gs => gs.CreateGame(testUser.Id)).Returns(game);
            var form = new MainForm(userServiceMock.Object, gameServiceMock.Object, testUser);

            form.BtnPlay_Click(null, EventArgs.Empty);

            gameServiceMock.Verify(gs => gs.CreateGame(testUser.Id), Times.Once());
        }

        [Fact]
        public void btnjoingame_Click_DoesNotJoin_WhenNoGamesAvailable()
        {
            gameServiceMock.Setup(gs => gs.GetAvailableGames()).Returns(new List<Game>()); // Пустой список игр
            var form = new MainForm(userServiceMock.Object, gameServiceMock.Object, testUser);

            form.btnjoingame_Click(null, EventArgs.Empty);

            gameServiceMock.Verify(gs => gs.JoinGame(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never());
        }

        [Fact]
        public void BtnPlay_Click_LogsError_WhenGameServiceNull()
        {
            var form = new MainForm(userServiceMock.Object, null, testUser, loggerMock.Object);

            form.BtnPlay_Click(null, EventArgs.Empty);

            loggerMock.Verify(l => l.Error("_gameService не инициализирован"), Times.Once());
            gameServiceMock.Verify(gs => gs.CreateGame(It.IsAny<Guid>()), Times.Never());
        }

        [Fact]
        public void LoadRatingTable_LogsInfo_WhenUsersLoaded()
        {
            userServiceMock.Setup(us => us.GetAllUsersSortedByRating()).Returns(users);
            var form = new MainForm(userServiceMock.Object, gameServiceMock.Object, testUser, loggerMock.Object);

            form.LoadRatingTable();

            loggerMock.Verify(l => l.Debug("Загрузка рейтинговой таблицы"), Times.Exactly(2));
            loggerMock.Verify(l => l.Info($"Рейтинговая таблица обновлена. Найдено пользователей: {users.Count}"), Times.Exactly(2));
        }
    }
}
