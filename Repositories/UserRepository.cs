using BudgetManagementApi.Data;
using BudgetManagementApi.Models.User;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<User> LoginAsync(User user)
        {
            User userDb = await _dbSet.FirstOrDefaultAsync(
                u => u.UserName == user.UserName && u.Email == user.Email
            );
            if (userDb is not null && BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password))
            {
                return userDb;
            }
            return null;
        }

        public async Task<User> RegisterAsync(User user)
        {
            try
            {
                User userDb = await _dbSet.FirstOrDefaultAsync(
                    u => u.UserName == user.UserName || u.Email == user.Email
                );
                if (userDb is not null)
                    throw new InvalidOperationException("Username or Email already taken");

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _dbSet.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
