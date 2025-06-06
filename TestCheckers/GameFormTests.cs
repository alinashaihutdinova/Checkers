using Checkers.Classes;
using Checkers.Core.Entities;
using Checkers.Core.Services;
using Checkers.Forms;
using Moq;

namespace TestCheckers
{
    public class GameFormTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IGameService> gameServiceMock;
        private readonly User testUser;
        private readonly Guid gameId;

        public GameFormTests()
        {
            userServiceMock = new Mock<IUserService>();
            gameServiceMock = new Mock<IGameService>();
            testUser = new User { Id = Guid.NewGuid(), Login = "TestUser" };
            gameId = Guid.NewGuid();

            var game = new Game
            {
                Id = gameId,
                Status = "Active",
                BlackPlayerId = Guid.NewGuid(),
                Turn = "Black",
                Moves = new List<Move>()
            };
            gameServiceMock.Setup(gs => gs.GetGameWithMoves(gameId)).Returns(game);
        }

        [Fact]
        public void EndCurrentTurn_ResetsChecker()
        {
            var form = new GameForm(userServiceMock.Object, gameServiceMock.Object, gameId, true, testUser.Id, testUser);
            var checker = new Checker { IsWhite = true, X = 2, Y = 2 };
            form.SelectedChecker = checker;
            form.AvailableMoves = new List<(int, int)> { (3, 3) };
            form.CurrentPlayerIsWhite = true;

            form.EndCurrentTurn();

            Assert.Null(form.SelectedChecker);
            Assert.Empty(form.AvailableMoves);
            Assert.False(form.CurrentPlayerIsWhite);
            Assert.Equal(30, form.TimeLeft);
        }

        [Fact]
        public void CheckAndStartMoveTimer_StopsTimer_WhenNotPlayersTurn()
        {
            gameServiceMock.Setup(gs => gs.IsPlayersTurn(gameId, testUser.Id)).Returns(false);
            var form = new GameForm(userServiceMock.Object, gameServiceMock.Object, gameId, true, testUser.Id, testUser);

            form.CheckAndStartMoveTimer();

            Assert.False(form.Timer.Enabled);
        }
    }
}
