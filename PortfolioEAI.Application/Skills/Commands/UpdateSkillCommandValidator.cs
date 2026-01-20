
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(x => x.SkillDto.Id)
                .NotEmpty().WithMessage("L'identifiant de la compétence est requis");
                
            RuleFor(x => x.SkillDto.Name)
                .NotEmpty().WithMessage("Le nom de la compétence est requis")
                .MaximumLength(100).WithMessage("Le nom de la compétence ne peut pas dépasser 100 caractères");

            RuleFor(x => x.SkillDto.Level)
                .NotEmpty().WithMessage("Le niveau de la compétence est requis")
                .MaximumLength(50).WithMessage("Le niveau de la compétence ne peut pas dépasser 50 caractères");

            RuleFor(x => x.SkillDto.Category)
                .NotEmpty().WithMessage("La catégorie de la compétence est requise")
                .MaximumLength(50).WithMessage("La catégorie de la compétence ne peut pas dépasser 50 caractères");
        }
    }
}