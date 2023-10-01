using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public class SavingDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Goal { get; set; }
    }
}
