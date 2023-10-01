using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models
{
    public class ExpenseProfil : Profile
    {
        public ExpenseProfil()
        {
            CreateMap<Expense, ExpenseDto>();
            CreateMap<ExpenseDto, Expense>();
        }
    }
}
