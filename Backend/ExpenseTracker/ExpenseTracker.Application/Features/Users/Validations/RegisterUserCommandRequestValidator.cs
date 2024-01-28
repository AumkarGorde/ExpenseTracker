using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class RegisterUserCommandRequestValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandRequestValidator() 
        {
            RuleFor(req=> req.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(req=> req.LastName)
                .NotEmpty().WithMessage("LastName is required.");

            RuleFor(request => request.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(request => request.Password)
           .NotEmpty()
           .MinimumLength(4)
           .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
           .WithMessage("Password must be at least 4 characters long, contain at least one letter, one number, and one special character.");
            
            // add validation for budget limit
            RuleFor(request => request.BudgetLimit)
            .GreaterThan(0)
            .LessThanOrEqualTo(5000000)
            .WithMessage("BudgetLimit must be greater than 0 and less than or equal to 5000000.");

            RuleFor(x => x.SavingsGoal)
            .Must((viewModel, savings) => viewModel.BudgetLimit > savings)
            .WithMessage("Savings goal must be less than budget limit.");
        }
    }
}
