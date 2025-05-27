using Checkers.Core.Entities;

namespace Checkers.Core.Services
{
    public interface IUserService
    {
        User? Authenticate(string login, string password);
        void RegisterUser(User user);
        string HashPassword(string password);
    }
}
