using FluentValidation;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Users.DTOs;

namespace PortfolioEAI.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    // Constructor to set up validation rules
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.DTO).SetValidator(new UserDtoValidator());   
    }
}
