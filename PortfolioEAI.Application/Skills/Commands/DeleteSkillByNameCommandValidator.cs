using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class DeleteSkillByNameCommandValidator : AbstractValidator<DeleteSkillsByName>
    {
        public DeleteSkillByNameCommandValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("The skill name must not be empty.");
        }
    }
}