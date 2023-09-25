using BudgetManagementApi.Models;
using FluentValidation;

namespace BudgetManagementApi.Validators
{
    public class InvestmentValidator : AbstractValidator<Investment>
    {
        public InvestmentValidator()
        {
            RuleFor(Investment => Investment.Amount)
                .LessThan(0)
                .WithMessage("Investment less than zero");
            RuleFor(Investment => Investment.Date).NotNull().WithMessage("Date is required");
        }
    }
}
