using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public DateTime Date{ get; set; }

        public virtual User User { get; set; }
    }
}