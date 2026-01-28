
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(x => x.SkillDto != null)
                .NotNull().WithMessage("La commande de mise à jour de la compétence ne peut pas être nulle");

            When(x => x.SkillDto != null, () =>
            {
                RuleFor(x => x.SkillDto.Id)
                    .NotEmpty().WithMessage("L'identifiant de la compétence est requis");
                    
                RuleFor(x => x.SkillDto.Name)
                    .NotEmpty().WithMessage("Le nom de la compétence est requis")
                    .MaximumLength(100).WithMessage("Le nom de la compétence ne peut pas dépasser 100 caractères");

                RuleFor(x => x.SkillDto.Level)
                    .MaximumLength(50).WithMessage("Le niveau de la compétence ne peut pas dépasser 50 caractères");

                RuleFor(x => x.SkillDto.Category)
                    .MaximumLength(50).WithMessage("La catégorie de la compétence ne peut pas dépasser 50 caractères");
            });
        }
    }
}