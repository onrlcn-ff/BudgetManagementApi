using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementApi.Models
{
    public class IncomeDto
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
