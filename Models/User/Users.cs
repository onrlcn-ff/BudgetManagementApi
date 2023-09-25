using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public byte[] PasswordHash{ get; set; }
        [Required]
        public string Email { get; set; }
        
        public List<Income> Incomes { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Saving> Savings { get; set; }
        public List<Investment> Investments { get; set; }
    }
}