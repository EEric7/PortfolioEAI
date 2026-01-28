using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(x => x.Dto).NotNull().WithMessage("Skill data must be provided.");

            When(x => x.Dto != null, () =>
            {
                RuleFor(x => x.Dto.Name)
                    .NotEmpty().WithMessage("The skill name is required")
                    .MaximumLength(100).WithMessage("The skill name must not exceed 100 characters");

                RuleFor(x => x.Dto.Level)
                    .MaximumLength(50).WithMessage("The skill level must not exceed 50 characters");

                RuleFor(x => x.Dto.Category)
                    .MaximumLength(50).WithMessage("The skill category must not exceed 50 characters");
            });
        }
    }
}