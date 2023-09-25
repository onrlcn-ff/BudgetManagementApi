using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using BudgetManagementApi.Models.User;
using FluentValidation;

namespace BudgetManagementApi.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Username Required").Length(4,20).WithMessage("Username must be between 4 and 20 characters.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("E-mail is required").EmailAddress().WithMessage("Invalid Email Address");
        }
    }
}