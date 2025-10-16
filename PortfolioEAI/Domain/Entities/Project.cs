using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Domain.Entities
{
    public class Project
    {
        /// <summary>
        /// Unique identifier for the project.
        /// This property represents the unique identifier for the project.
        /// It is a required field and should be a valid GUID.
        /// </summary>
        public Guid Id { get; private set; }

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
        /// URL of the project image.
        /// This property represents the URL of an image associated with the project.
        /// It is typically used to display a visual representation of the project, such as a screenshot or logo.
        /// The image URL should be a valid absolute URI that points to the location of the image file.
        /// It is important to ensure that the image URL is accessible and points to a valid image file format (e.g., JPEG, PNG).
        /// This property is used to enhance the visual appeal of the project and provide users with a quick overview of its appearance.
        /// </summary>
        public string ImageUrl { get; private set; } = string.Empty;

        /// <summary>
        /// URL of the project.
        /// This property represents the URL of the project, which can be a link to the project's website, repository, or any other relevant location.
        /// It is typically used to provide users with a way to access the project directly, allowing them to explore its features, documentation, or source code.
        /// The project URL should be a valid absolute URI that points to the location of the project.
        /// It is important to ensure that the project URL is accessible and points to a valid resource.
        /// This property is used to facilitate easy access to the project for users who are interested in learning more about it or contributing to its development.
        /// </summary>
        public ObjectUrl Url { get; private set; } = default!;

        // Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Project() : base() { } 
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the Project class with a specified identifier.
        /// The title, description, image URL, and project URL are required parameters.
        /// This constructor is typically used when the project ID is already known, such as when retrieving
        /// an existing project from a database. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="projectUrl"></param>
        public Project(Guid id, string title, string description, string imageUrl, string url)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetImage(imageUrl);
            SetUrl(url);
        }

        /// <summary>
        /// Sets the title of the project.
        /// Throws a BusinessRuleViolationException if the title is null or whitespace. 
        /// </summary>
        /// <param name="title"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessRuleViolationException("Le titre est requis.");
            Title = title;
        }

        /// <summary>
        /// Sets the description of the project.
        /// Throws a BusinessRuleViolationException if the description is null or whitespace.
        /// </summary>
        /// <param name="description"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessRuleViolationException("La description est requise.");
            Description = description;
        }

        /// <summary>
        /// Sets the image URL of the project.
        /// Throws a BusinessRuleViolationException if the image URL is not a valid absolute URI.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetImage(string imageUrl)
        {
#if !DEBUG
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                throw new BusinessRuleViolationException("L'URL d'image est invalide.");
#endif
            ImageUrl = imageUrl;
        }
        
        /// <summary>
        /// Sets the URL of the project.
        /// Throws a BusinessRuleViolationException if the URL is not a valid absolute URI.
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetUrl(string url)
        {
            Url = new ObjectUrl(url);
        }
    }
}