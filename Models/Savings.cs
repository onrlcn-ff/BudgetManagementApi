using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public class Saving
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string Goal { get; set; }
        public virtual User.User User { get; set; }
    }
}
