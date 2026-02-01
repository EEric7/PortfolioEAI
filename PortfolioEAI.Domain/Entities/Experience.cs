using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Domain.Entities
{
    public class Experience : AggregateRoot
    {
        /// <summary>
        /// Collection of projects associated with the experience.
        /// This property represents a collection of projects that are related to the experience.
        /// It allows for the association of multiple projects with a single experience,
        /// enabling better organization and retrieval of related information.
        /// </summary>
        private readonly List<Project> _projects = new();
    
        /// <summary>
        /// Name of the company where the experience was gained.
        /// This property represents the name of the company or organization where the experience was gained.
        /// It is a required field and should be descriptive enough to give an idea of the company's identity.
        /// </summary>
        public string Company { get; private set; } = string.Empty;

        /// <summary>
        /// Description of the experience.
        /// This property provides a detailed description of the experience, including the tasks performed,
        /// skills acquired, and any notable achievements.
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        /// <summary>
        /// URL of the image representing the experience.  
        /// This property holds the URL of an image associated with the experience.
        /// It is typically used to display a visual representation of the experience in user interfaces.
        /// </summary>
        public StoredFile? Image { get; private set; } = default;

        /// <summary>
        /// Collection of projects associated with the experience.
        /// This property represents a collection of projects that are related to the experience.
        /// It allows for the association of multiple projects with a single experience,
        /// enabling better organization and retrieval of related information.
        /// </summary>
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

// Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Experience() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Creates a new instance of the Experience class with the specified parameters.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="position"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Experience Create(string company, string description)
        {
            var experience = new Experience();
            experience.SetCompany(company);
            experience.SetDescription(description);
            return experience;
        }

        /// <summary>
        /// Sets the company name for the experience.
        /// This method allows you to set the company name for the experience.
        /// It is important to ensure that the company name being set is not null or empty,
        /// and meets the length requirements.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCompany(string? value)
        {
            try
            {
                Company = ValidateCompany(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

       /// <summary>
       ///  Sets the description for the experience.
       ///  This method allows you to set the description for the experience.
       ///  It is important to ensure that the description being set is not null or empty,
       ///  and meets the length requirements.
       /// </summary>
       /// <param name="value"></param>
       /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetDescription(string? value)
        {
            try
            {
                Description = ValidateDescription(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Adds multiple projects to the experience.
        /// This method allows you to associate multiple projects with the experience.
        /// It iterates through the provided collection of projects and adds each one individually.
        /// </summary>
        /// <param name="projects"></param>
        public void AddProject(IEnumerable<Project> projects)
        {
            foreach (var project in projects)
            {
                AddProject(project);
            }
        }

       /// <summary>
       /// Adds a project to the experience.
       /// This method allows you to associate a project with the experience.
       /// It is important to ensure that the project being added is not null and not already associated with the experience.
       /// </summary>
       /// <param name="value"></param>
       /// <exception cref="BusinessRuleViolationException"></exception>
        public bool AddProject(Project value)
        {
            try
            {
                if (_projects.Contains(value))
                    return false;

                _projects.Add(value);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Removes a project from the experience.
        /// This method allows you to disassociate a project from the experience.
        /// It is important to ensure that the project being removed is not null and is currently associated with the experience.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void RemoveProject(Project value)
        {
            try
            {
                if (!Projects.Contains(value))
                    throw new BusinessRuleViolationException("Project not found in the experience.", new ArgumentException(nameof(value)));

                _projects.Remove(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        private static string ValidateDescription(string? value)
        {
            // Descrition bulk can be empty, but not null
            return value?? string.Empty;
        }

        private static string ValidateCompany(string? value)
        {
            return value?? string.Empty;
        }
    }
}