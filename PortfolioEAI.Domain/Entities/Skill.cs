using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Enums;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.Entities
{
    public class Skill : AggregateRoot
    {
        /// <summary>
        /// Name of the skill.
        /// This property represents the name or title of the skill.
        /// It is a required field and should be descriptive enough to give an idea of what the skill is about.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Description of the skill.
        /// This property provides a detailed description of the skill, including its purpose, features, and any other relevant information.
        /// /// It is a required field and should be descriptive enough to give an idea of what the skill is about.
        /// </summary> 
        public SkillLevel Level { get; private set; } = SkillLevel.None;

        /// <summary>
        /// Category of the skill.
        /// This property represents the category to which the skill belongs, such as frontend, backend,
        /// fullstack, devops, etc.
        /// It is used to classify the skill for better organization and retrieval in applications,
        /// such as in a portfolio, resume, or skills management system.
        /// </summary>
        public SkillCategory Category { get; private set; } = SkillCategory.None;

        // Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Skill() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Creates a new Skill instance with the specified name, level, and category.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static Skill Create(string? name, string? level, string? category)
        {
            var skill = new Skill();
            skill.SetName(name);
            skill.SetLevel(level);
            skill.SetCategory(category);
            return skill;
        }

        /// <summary>
        /// Sets the name of the skill.
        /// This method allows you to change the name of the skill to a new value.
        /// It validates that the name is not null, empty, or too short/long.
        /// If the name is invalid, it throws a BusinessRuleViolationException.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetName(string? value)
        {
            try
            {
                Name = ValidateName(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the level of the skill.
        /// This method allows you to change the level of the skill to a new value.
        /// It validates that the level is a defined value in the SkillLevel enum.
        /// If the level is not defined, it throws a BusinessRuleViolationException.
        /// </summary>
        /// <param name="level"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>       
        public void SetLevel(string? level)
        {
            try
            {
                Level = ValidateLevel(level);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// Sets the category of the skill.
        /// This method allows you to change the category of the skill to a new value.
        /// It validates that the category is a defined value in the SkillCategory enum.
        /// If the category is not defined, it throws a BusinessRuleViolationException.
        /// </summary>
        /// <param name="category"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCategory(string? value)
        {
            try
            {
                Category = ValidateCategory(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Validates the provided skill level.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static SkillLevel ValidateLevel(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The skill level is required.", new ArgumentNullException(nameof(value)));

            return value  switch
            {
                "beginner" => SkillLevel.Beginner,
                "intermediate" => SkillLevel.Intermediate,
                "advanced" => SkillLevel.Advanced,
                "expert" => SkillLevel.Expert,
                _ => SkillLevel.None,
            };  
        }

        /// <summary>
        /// Validates the provided skill category.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static SkillCategory ValidateCategory(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The skill category is required.", new ArgumentNullException(nameof(value)));

            return value switch
            {
                "frontend" => SkillCategory.Frontend,
                "backend" => SkillCategory.Backend,
                "fullstack" => SkillCategory.Fullstack,
                "devops" => SkillCategory.DevOps,
                _ => SkillCategory.None,
            };
        }

        /// <summary>
        /// Validates the provided skill name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static string ValidateName(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The skill name is required.", new ArgumentNullException(nameof(value)));

            return value;
        }
    }
}