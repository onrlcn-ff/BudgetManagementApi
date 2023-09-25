using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using FluentValidation;

namespace BudgetManagementApi.Validators
{
    public class ExpenseValidator : AbstractValidator<Expense>
    {
        public ExpenseValidator()
        {
            RuleFor(expense => expense.Amount).LessThan(0).WithMessage("Amount less than zero");
            RuleFor(expense => expense.Category).NotNull().WithMessage("Category is required");
            RuleFor(expense => expense.Date).NotNull().WithMessage("Date is required");
        }
    }
}