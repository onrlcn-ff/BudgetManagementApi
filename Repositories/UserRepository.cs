using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Data;
using BudgetManagementApi.Models;
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
            User userDb = _dbSet.FirstOrDefault(u => u.UserName == user.UserName);
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
                User userDb = _dbSet.FirstOrDefault(u => u.UserName == user.UserName);
                if (userDb is not null)
                    throw new InvalidOperationException("Username already taken");

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
