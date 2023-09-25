using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using FluentValidation;

namespace BudgetManagementApi.Validators
{
    public class IncomeValidator : AbstractValidator<Income>
    {
        public IncomeValidator()
        {
            RuleFor(income => income.Amount).LessThan(0).WithMessage("Amount less than zero");
            RuleFor(income => income.Date).NotNull().WithMessage("Date is required.");
        }
    }
}