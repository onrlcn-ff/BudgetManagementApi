using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetManagementApi.Models
{
    public enum InvestmentType{
        Bank= 1,
        Borsa
    }
    public class Investment
    {
        public int InvestmentsId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public InvestmentType Type { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }
}