using PortfolioEAI.Application.StoredFiles.DTOs;

namespace PortfolioEAI.Application.DTOs
{
    public class ExperienceDto
    {
        /// <summary>
        /// Unique identifier for the experience.
        /// This property is used to uniquely identify an experience record in the system.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Company name associated with the experience.
        /// This property holds the name of the company or organization where the experience was gained.
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// Description of the experience.
        /// This property provides a detailed description of the experience, including responsibilities,
        /// achievements, and any other relevant information.
        /// </summary>
        public string? Description { get; set; }
        

        /// <summary>
        /// URL of the image associated with the experience.
        /// </summary>
        public StoredFileDto? Enseigne { get; set; }

        /// <summary>
        /// List of project identifiers associated with the experience.
        /// </summary>
        public IList<ProjectDto> Projects { get; set; } = [];

        public ExperienceDto() { }

        public ExperienceDto(ExperienceDto experienceDto)
        {
            Id = experienceDto.Id;
            Company = experienceDto.Company;
            Description = experienceDto.Description;
            Enseigne = experienceDto.Enseigne;
            Projects = experienceDto.Projects;
        }
    }
}