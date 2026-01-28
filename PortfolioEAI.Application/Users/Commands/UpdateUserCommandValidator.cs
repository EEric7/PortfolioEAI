using FluentValidation;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Users.DTOs;

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
                RuleFor(x => x.UserDto).SetValidator(new UserDtoValidator());
            });
        }
    }
}