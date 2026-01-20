using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    // Constructor to set up validation rules
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email est requis")
            .EmailAddress().WithMessage("L'email n'est pas valide");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis")
            .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères");

        RuleFor(x => x.UserName)
            .MaximumLength(100).WithMessage("Le nom d'affichage ne peut pas dépasser 100 caractères");

        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("Le prénom ne peut pas dépasser 50 caractères");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Le nom de famille ne peut pas dépasser 50 caractères");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La description ne peut pas dépasser 500 caractères");        
        
    }
}
