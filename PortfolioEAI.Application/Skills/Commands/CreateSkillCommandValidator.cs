using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The skill name is required")
                .MaximumLength(100).WithMessage("The skill name must not exceed 100 characters");

            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("The skill level is required")
                .MaximumLength(50).WithMessage("The skill level must not exceed 50 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("The skill category is required")
                .MaximumLength(50).WithMessage("The skill category must not exceed 50 characters");
        }
    }
}