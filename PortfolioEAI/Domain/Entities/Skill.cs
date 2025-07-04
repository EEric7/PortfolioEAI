using PortfolioEAI.Domain.Enums;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.Entities
{
    public class Skill
    {
        /// <summary>
        /// Unique identifier for the skill.
        /// This property represents the unique identifier for the skill.
        /// It is a required field and should be a valid GUID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the skill.
        /// This property represents the name or title of the skill.
        /// It is a required field and should be descriptive enough to give an idea of what the skill is about.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the skill.
        /// This property provides a detailed description of the skill, including its purpose, features, and any other relevant information.
        /// /// It is a required field and should be descriptive enough to give an idea of what the skill is about.
        /// </summary> 
        public SkillLevel Level { get; private set; }

        /// <summary>
        /// Category of the skill.
        /// This property represents the category to which the skill belongs, such as frontend, backend,
        /// fullstack, devops, etc.
        /// It is used to classify the skill for better organization and retrieval in applications,
        /// such as in a portfolio, resume, or skills management system.
        /// </summary>
        public SkillCategory Category { get; private set; }

        // Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Skill() : base() { } 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the Skill class with the specified parameters.
        /// This constructor allows you to create a skill with a specific ID, name, level, and category.
        /// </summary>
        /// <param name="id">The unique identifier for the skill. If null, a new GUID will be generated.</param>
        /// <param name="name">The name of the skill.</param>
        /// <param name="level">The level of the skill, represented by the SkillLevel enum.</param>
        /// <param name="category">The category of the skill, represented by the SkillCategory enum.</param>
        /// <exception cref="BusinessRuleViolationException">Thrown when the name is null or empty.</exception>
        public Skill(Guid id, string? name, SkillLevel level, SkillCategory category)
        {
            Id = id;
            Name = name?? string.Empty;
            Level = level;
            Category = category;
        }

        /// <summary>
        /// Sets the name of the skill.
        /// This method allows you to change the name of the skill to a new value.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCompany(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRuleViolationException("The company name is required.");
            Name = name;
        }

        /// <summary>
        /// Sets the level of the skill.
        /// This method allows you to change the level of the skill to a new value.
        /// It validates that the level is a defined value in the SkillLevel enum.
        /// If the level is not defined, it throws a BusinessRuleViolationException.
        /// </summary>
        /// <param name="level"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetLevel(SkillLevel level)
        {
            if (!Enum.IsDefined(typeof(SkillLevel), level))
                throw new BusinessRuleViolationException("Invalid skill level.");
            Level = level;
        }

        /// <summary>
        /// Sets the category of the skill.
        /// This method allows you to change the category of the skill to a new value.
        /// It validates that the category is a defined value in the SkillCategory enum.
        /// If the category is not defined, it throws a BusinessRuleViolationException.
        /// </summary>
        /// <param name="category"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCategory(SkillCategory category)
        {
            if (!Enum.IsDefined(typeof(SkillCategory), category))
                throw new BusinessRuleViolationException("Invalid skill category.");
            Category = category;
        }

    }
}