using PortfolioEAI.Web.Domain.Exceptions;

namespace PortfolioEAI.Web.Domain.Entities
{
    public class Experience
    {
        /// <summary>
        /// Unique identifier for the experience.
        /// This property represents the unique identifier for the experience.
        /// It is a required field and should be a valid GUID.
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Name of the company where the experience was gained.
        /// This property represents the name of the company or organization where the experience was gained.
        /// It is a required field and should be descriptive enough to give an idea of the company's identity.
        /// </summary>
        public string Company { get; private set; } = string.Empty;

        /// <summary>
        /// Position held during the experience.
        /// This property represents the position or job title held during the experience.
        /// It is a required field and should be descriptive enough to give an idea of the role
        /// and responsibilities associated with the experience.
        /// </summary>
        public string Position { get; private set; } = string.Empty;

        /// <summary>
        /// Start date of the experience.
        /// This property represents the date when the experience started.
        /// It is a required field and should be a valid date.
        /// The start date is stored as a DateOnly type, which represents a date without a time component.
        /// </summary>
        public DateOnly StartDate { get; private set; }

        /// <summary>
        /// End date of the experience.
        /// This property represents the date when the experience ended.
        /// It can be null if the experience is ongoing or has not yet ended.
        /// </summary>
        public DateOnly? EndDate { get; private set; }

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
        public string ImageUrl { get; private set; } = string.Empty;

        /// <summary>
        /// Collection of projects associated with the experience.
        /// This property represents a collection of projects that are related to the experience.
        /// It allows for the association of multiple projects with a single experience,
        /// enabling better organization and retrieval of related information.
        /// </summary>
        public IList<Project> Projects { get; private set; } = new List<Project>();

// Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Experience() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the <see cref="Experience"/> class with the specified parameters.
        /// This constructor allows you to create an experience with a specific ID, company name, position,
        /// start date, end date, description, and image URL.
        /// </summary>
        /// <param name="id">The unique identifier for the experience.</param>
        /// <param name="company">The name of the company where the experience was gained.</param>
        /// <param name="position">The position held during the experience.</param>
        /// <param name="startDate">The start date of the experience.</param>
        /// <param name="endDate">The end date of the experience. It can be null if the experience is ongoing.</param>
        /// <param name="description">The description of the experience.</param>
        /// <param name="imageUrl">The URL of the image representing the experience.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the required parameters (company, position, startDate, description, or imageUrl) are null or empty.</exception>
        /// <exception cref="BusinessRuleViolationException">Thrown when the start date or end date is the default value (DateOnly.MinValue), or when the image URL is not a valid absolute URI in debug mode.</exception>>
        public Experience(Guid id, string company, string position, DateOnly startDate, DateOnly? endDate, string description, string imageUrl, IList<Project> projects)
        {
            Id = id;
            SetCompany(company);
            SetPosition(position);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetDescription(description);
            SetImageUrl(imageUrl);
            
            foreach (var project in projects)
                AddProject(project);
        }
        
        /// <summary>
        /// Sets the company name for the experience.
        /// This method allows you to set the company name for the experience.
        /// It is important to ensure that the company name being set is not null or empty.
        /// </summary>
        /// <param name="company"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetCompany(string company)
        {
            if (string.IsNullOrWhiteSpace(company))
                throw new BusinessRuleViolationException("The company name is required.");
            Company = company;
        }
        
        /// <summary>
        /// Sets the company name for the experience.
        /// This method allows you to set the company name for the experience.
        /// It is important to ensure that the company name being set is not null or empty.
        /// </summary>
        /// <param name="position"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetPosition(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
                throw new BusinessRuleViolationException("The position is required.");
            Position = position;
        }

        /// <summary>
        /// Sets the start date for the experience.
        /// This method allows you to set the start date for the experience.
        /// It is important to ensure that the start date being set is not the default value (DateOnly.MinValue).
        /// If the start date is the default value, a BusinessRuleViolationException will be thrown.
        /// The start date will be stored in the StartDate property of the experience.
        /// </summary>
        /// <param name="startDate"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetStartDate(DateOnly? startDate)
        {
            if (startDate == null || startDate == DateOnly.MinValue)
                throw new BusinessRuleViolationException("Start date cannot be null or default value.");
            StartDate = startDate.Value;
        }

        /// <summary>
        /// Sets the end date for the experience.
        /// This method allows you to set the end date for the experience.
        /// It is important to ensure that the end date being set is not the default value (DateOnly.MinValue).
        /// If the end date is the default value, a BusinessRuleViolationException will be thrown.
        /// The end date will be stored in the EndDate property of the experience.
        /// If the end date is null, it indicates that the experience is ongoing or has not yet ended.
        /// </summary>
        /// <param name="endDate"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetEndDate(DateOnly? endDate)
        {
            if (endDate == default)
                throw new BusinessRuleViolationException("End date cannot be default value.");
            EndDate = endDate;
        }

        /// <summary>
        /// Sets the description for the experience.
        /// This method allows you to set the description for the experience.
        /// It is important to ensure that the description being set is not null or empty.
        /// If the description is null or empty, a BusinessRuleViolationException will be thrown.
        /// The description will be stored in the Description property of the experience.
        /// </summary>
        /// <param name="description"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessRuleViolationException("Description is requeried.");
                
            Description = description;
        }

        /// <summary>
        /// Sets the image URL for the experience.
        /// This method allows you to set the image URL for the experience.
        /// It is important to ensure that the image URL being set is a valid absolute URI.
        /// If the image URL is not a valid absolute URI, a BusinessRuleViolationException will be thrown.
        /// The image URL will be stored in the ImageUrl property of the experience.
        /// In debug mode, the method checks if the image URL is well-formed.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetImageUrl(string imageUrl)
        {
#if !DEBUG
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                throw new BusinessRuleViolationException("The image URL must be valid.");
#endif
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Adds a project to the experience.
        /// This method allows you to associate a project with the experience.
        /// It is important to ensure that the project being added is not null.
        /// If the project is null, an ArgumentNullException will be thrown.
        /// The project will be added to the Projects collection of the experience.
        /// </summary>
        /// <param name="project"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddProject(Project project)
        {
            if (project == null)
                throw new BusinessRuleViolationException("Project must't be null");
            Projects.Add(project);
        }

        /// <summary> 
        /// Adds a project to the experience.
        /// This method allows you to associate a project with the experience.
        /// It is important to ensure that the project being added is not null.
        /// If the project is null, an ArgumentNullException will be thrown.
        /// The project will be added to the Projects collection of the experience.
        /// </summary>
        /// <param name="project"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveProject(Project project)
        {
            if (project == null)
                throw new BusinessRuleViolationException("Project must't be null");

            Projects.Remove(project);
        }
    }
}