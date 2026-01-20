using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;
using PortfolioEAI.Domain.Common;

namespace PortfolioEAI.Domain.Entities
{
    public class Project : AggregateRoot
    {
        /// <summary>
        /// Title of the project.
        /// This property represents the name or title of the project.
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// Description of the project.
        /// This property provides a detailed description of the project, including its purpose, features, and any other relevant information.
        /// It is typically used to give users an understanding of what the project is about and what it aims to achieve.
        /// The description should be concise yet informative, allowing users to grasp the essence of the project quickly.
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        /// <summary>
        /// Start date of the project.
        /// This property represents the date when the project started.
        /// It is a required field and should be a valid date.
        /// The start date is stored as a DateOnly type, which represents a date without a time component.
        /// </summary>
        public DateOnly StartDate { get; private set; }

        /// <summary>
        /// End date of the project.
        /// This property represents the date when the project ended.
        /// It can be null if the project is ongoing or has not yet ended. 
        /// </summary>
        public DateOnly? EndDate { get; private set; }

        /// <summary>
        /// URL of the project image.
        /// This property represents the URL of an image associated with the project.
        /// It is typically used to display a visual representation of the project, such as a screenshot or logo.
        /// The image URL should be a valid absolute URI that points to the location of the image file.
        /// It is important to ensure that the image URL is accessible and points to a valid image file format (e.g., JPEG, PNG).
        /// This property is used to enhance the visual appeal of the project and provide users with a quick overview of its appearance.
        /// </summary>
        public Photo? ImageUrl { get; private set; } = default!;

        /// <summary>
        /// URL of the project.
        /// This property represents the URL of the project, which can be a link to the project's website, repository, or any other relevant location.
        /// It is typically used to provide users with a way to access the project directly, allowing them to explore its features, documentation, or source code.
        /// The project URL should be a valid absolute URI that points to the location of the project.
        /// It is important to ensure that the project URL is accessible and points to a valid resource.
        /// This property is used to facilitate easy access to the project for users who are interested in learning more about it or contributing to its development.
        /// </summary>
        public ObjectUrl? Url { get; private set; } = default!;

        // Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Project() : base() { } 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Creates a new Project instance with the specified title and description.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Project Create(string title, string description, DateOnly startDate, DateOnly? endDate)
        {
            var project = new Project();
            project.SetTitle(title);
            project.SetStartDate(startDate);
            project.SetEndDate(endDate);
            project.SetDescription(description);
            return project;
        }

        /// <summary>
        /// Sets the title of the project.
        /// Throws a BusinessRuleViolationException if the title is null or whitespace.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        /// <returns></returns>
        public void SetTitle(string value)
        {
            try
            {
                Title = ValidateTitle(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
       /// Sets the start date for the experience.
       /// This method allows you to set the start date for the experience.
       /// It is important to ensure that the start date being set is not null and is before the end date (if provided).
       /// </summary>
       /// <param name="value"></param>
       /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetStartDate(DateOnly? value)
        {
            try
            {
                StartDate = ValidateStartDate(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the end date for the experience.
        /// This method allows you to set the end date for the experience.
        /// It is important to ensure that the end date being set is not before the start date
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetEndDate(DateOnly? value)
        {
            try
            {
                EndDate = ValidateEndDate(value, StartDate);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the description of the project.
        /// Throws a BusinessRuleViolationException if the description is null or whitespace.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetDescription(string value)
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
        /// Sets the URL of the project.
        /// This method allows you to set the URL of the project.
        /// It is important to ensure that the URL being set is a valid absolute URI.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetUrl(string value)
        {
            try
            {
                if(Url == default)
                    Url = ObjectUrl.Create(value);
            
                Url.SetValue(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }   

        private string ValidateTitle(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The title is required.", new ArgumentNullException(nameof(value)));

            if (value!.Length < 2)
                throw new BusinessRuleViolationException("The title must be at least 2 characters long.", new ArgumentException(nameof(value)));    
            
            if (value.Length > 100)
                throw new BusinessRuleViolationException("The title must not exceed 100 characters.", new ArgumentException(nameof(value)));

            return value;
        }

        private string ValidateDescription(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The description is required.", new ArgumentNullException(nameof(value)));

            if (value!.Length < 10)
                throw new BusinessRuleViolationException("The description must be at least 10 characters long.", new ArgumentException(nameof(value)));

            if (value.Length > 500)
                throw new BusinessRuleViolationException("The description must not exceed 500 characters.", new ArgumentException(nameof(value)));

            return value;
        }
         private static DateOnly ValidateStartDate(DateOnly? value)
        {
            if (value == null || value == DateOnly.MinValue)
                throw new BusinessRuleViolationException("Start date cannot be null or default value.", new ArgumentNullException(nameof(value)));

            return value.Value;
        }

        private static DateOnly? ValidateEndDate(DateOnly? value, DateOnly startDate)
        {

            if (value != null && value < startDate)
                throw new BusinessRuleViolationException("End date cannot be earlier than start date.", new ArgumentException(nameof(value)));

            return value;
        }
    }
}