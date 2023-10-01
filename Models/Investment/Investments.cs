using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public enum InvestmentType
    {
        Bank = 1,
        Borsa
    }

    public class Investment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public InvestmentType Type { get; set; }
        public DateTime Date { get; set; }
        public virtual User.User User { get; set; }
    }
}
