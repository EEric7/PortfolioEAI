using FluentValidation;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Skills.DTOs
{
    public class SkillDtoValidator : AbstractValidator<SkillDto>
    {
        public SkillDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom de la compétence est requis.")
                .MaximumLength(100).WithMessage("Le nom de la compétence ne peut pas dépasser 100 caractères.");
        }
    }
}