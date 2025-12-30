
namespace PortfolioEAI.Web.Application.DTOs
{
    public class ExperienceDto
    {
        /// <summary>
        /// Unique identifier for the experience.
        /// This property is used to uniquely identify an experience record in the system.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the experience.
        /// This property represents the name or title of the experience, such as a job title or
        /// position held during the experience.
        /// </summary>             
        public string? Title { get; set; }

        /// <summary>
        /// Company name associated with the experience.
        /// This property holds the name of the company or organization where the experience was gained.
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// Position of the experience.
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Start date of the experience.
        /// This property represents the date when the experience started.
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End date of the experience.
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Description of the experience.
        /// This property provides a detailed description of the experience, including responsibilities,
        /// achievements, and any other relevant information.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// URL of the image associated with the experience.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// List of project identifiers associated with the experience.
        /// </summary>
        public IList<ProjectDto> Projects { get; set; } = new List<ProjectDto>();

        public ExperienceDto() { }

        public ExperienceDto(ExperienceDto experienceDto)
        {
            Id = experienceDto.Id;
            Title = experienceDto.Title;
            Company = experienceDto.Company;
            Position = experienceDto.Position;
            StartDate = experienceDto.StartDate;
            EndDate = experienceDto.EndDate;
            Description = experienceDto.Description;
            ImageUrl = experienceDto.ImageUrl;
            Projects = experienceDto.Projects;
        }
    }
}