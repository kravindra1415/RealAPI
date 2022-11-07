using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        }
    }
}
