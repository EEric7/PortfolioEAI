using FluentValidation;
using PortfolioEAI.Application.Interfaces.UserPorts;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddSkillUserCommandValidator : AbstractValidator<AddSkillUserCommand>
    {
        // Constructor to set up validation rules
        public AddSkillUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("L'ID utilisateur est requis.");

            RuleFor(x => x.SkillIDs)
                .NotEmpty().WithMessage("Au moins une compétence doit être fournie.");

            When(x => x.SkillIDs != null, () =>
            {
                RuleForEach(x => x.SkillIDs)
                    .NotEmpty().WithMessage("Chaque ID de compétence doit être valide.");
            });
        }
    }
}