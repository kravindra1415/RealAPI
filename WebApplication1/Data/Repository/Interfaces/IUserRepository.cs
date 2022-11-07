using WebApplication1.Models;

namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
    }
}
