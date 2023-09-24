using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using FluentValidation;

namespace BudgetManagementApi.Validators
{
    public class SavingValidator : AbstractValidator<Saving>
    {
        public SavingValidator()
        {
            RuleFor(Saving=> Saving.Amount).LessThan(0).WithMessage("Saving less than zero.");
        }
    }
}