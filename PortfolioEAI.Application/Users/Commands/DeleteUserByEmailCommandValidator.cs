using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Users.Commands
{
    public class DeleteUserByEmailCommandValidator : AbstractValidator<DeleteUserByEmailCommand>
    {
        // Constructor to set up validation rules
        public DeleteUserByEmailCommandValidator()
        {
            RuleFor(x => x.Email)
            .NotNull().WithMessage("L'email ne peut pas être nul")
            .EmailAddress().WithMessage("L'email n'est pas valide");
        }
    }
}