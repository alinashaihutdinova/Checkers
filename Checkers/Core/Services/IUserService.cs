using Checkers.Core.Entities;

namespace Checkers.Core.Services
{
    public interface IUserService
    {
        User? Authenticate(string login, string password);
        void RegisterUser(User user);
        string HashPassword(string password);
        List<Game> GetUserGameHistory(Guid userId);
        void UpdateUserStats(Guid userId, bool isWin);
        List<User> GetAllUsersSortedByRating();
        int CalculateRating(User user);
        User GetUserById(Guid id);
    }
}
