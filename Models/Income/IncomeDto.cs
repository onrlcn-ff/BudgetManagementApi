using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementApi.Models
{
    public class IncomeDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
