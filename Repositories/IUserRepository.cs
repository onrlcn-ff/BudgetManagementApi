using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using BudgetManagementApi.Models.User;

namespace BudgetManagementApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(User user);
        Task<User> RegisterAsync(User user);
        
    }
}