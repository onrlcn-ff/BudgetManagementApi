using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public class InvestmentDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public InvestmentType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
