using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public enum ExpenseCategory{
        Food = 1,
        Drink,
        Fun
    }
    public class Expense
    {
        [Key]
        public int ExpensesId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public ExpenseCategory Category{ get; set; }
        public DateTime Date{ get; set; }
        public virtual User.User User { get; set; }
    }
}