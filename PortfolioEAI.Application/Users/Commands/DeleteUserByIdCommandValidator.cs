
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Users.Commands
{
    public class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
    {
        public DeleteUserByIdCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotNull().WithMessage("L'ID est requis");
        }
    }
}