using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserDto)
                .NotNull().WithMessage("Le DTO de l'utilisateur ne peut pas être nul.");

            When(x => x.UserDto != null, () =>
            {
                RuleFor(x => x.UserDto!.Email)
                    .NotEmpty().WithMessage("L'email est requis")
                    .EmailAddress().WithMessage("L'email n'est pas valide");
            });
        }
    }
}