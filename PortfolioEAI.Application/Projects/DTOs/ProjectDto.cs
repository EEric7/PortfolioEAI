namespace PortfolioEAI.Application.DTOs
{
    public class ProjectDto
    {
        /// <summary>
        /// Unique identifier for the project.
        /// This property is used to uniquely identify a project in the system.
        /// It is typically a GUID (Globally Unique Identifier) that is generated when the project is created.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the project.
        /// This property represents the name or title of the project.
        /// It is a required field and should be descriptive enough to give an idea of what the project is about.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the project.
        /// This property provides a detailed description of the project, including its purpose, features, and any other relevant information.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Start date of the project.
        /// This property represents the date when the project started.
        /// </summary>
        public DateOnly StartDate { get; set; }

        /// <summary>
        /// End date of the project.
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// URL of the project image.
        /// This property holds the URL of an image associated with the project.
        /// It is typically used to display a visual representation of the project in user interfaces.
        /// The URL should point to a valid image resource.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// URL of the project.
        /// This property represents the URL where the project can be accessed or viewed.
        /// It is typically a web address that points to the project's homepage or repository.
        /// This URL is used to provide users with direct access to the project online.
        /// It is important that this URL is valid and accessible.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        public ProjectDto() {}
        
        public ProjectDto(ProjectDto dto)
        {
            Id = dto.Id;
            Title = dto.Title;
            Description = dto.Description;
            ImageUrl = dto.ImageUrl;
            Url = dto.Url;
        }
    }
}