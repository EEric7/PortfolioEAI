using System.Data;
using FluentValidation;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Users.DTOs
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Le nom d'utilisateur est requis.")
                .MaximumLength(50).WithMessage("Le nom d'utilisateur ne peut pas dépasser 50 caractères.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Le mot de passe est requis.")
                .MinimumLength(6).WithMessage("Le mot de passe doit contenir au moins 6 caractères.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est requis.")
                .EmailAddress().WithMessage("L'email n'est pas valide.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Le rôle est requis.")
                .MaximumLength(50).WithMessage("Le rôle ne peut pas dépasser 50 caractères.");
        }
    }
}