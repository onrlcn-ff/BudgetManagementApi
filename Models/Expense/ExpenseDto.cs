using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public class ExpenseDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public ExpenseCategory Category { get; set; }
        public DateTime Date { get; set; }
    }
}
