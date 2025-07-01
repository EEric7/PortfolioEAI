using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Domain.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdProject")]
        [Required]
        [Display(Name = "Project ID")]
        [Description("Unique identifier for the project")]
        [JsonIgnore]
        public Guid Id { get; private set; }

        [Required]
        [Column("Title")]
        [MaxLength(200)]
        [MinLength(5)]
        [Display(Name = "Project Title")]
        [Description("Title of the project")]
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonInclude]
        [JsonRequired]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [DataType(DataType.Text)]
        public string Title { get; private set; }

        [Column("Description")]
        [MaxLength(1000)]
        [MinLength(10)] 
        [Display(Name = "Project Description")]
        [Description("Description of the project")]
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonInclude]
        [JsonRequired]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [DataType(DataType.MultilineText)]
        public string Description { get; private set; }

        [Column("ImageUrl")]
        [Display(Name = "Project Image URL")]
        [Description("URL of the project image")]
        [JsonPropertyName("imageUrl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonInclude]
        [JsonRequired]
        [DataType(DataType.ImageUrl)]
        [Url(ErrorMessage = "L'URL de l'image doit être valide.")]
        [MaxLength(500)]
        [MinLength(5)]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string ImageUrl { get; private set; }

        [Column("Url")]
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonInclude]
        [JsonRequired]
        [Display(Name = "Project URL")]
        [Description("URL of the project")]
        [DataType(DataType.Url)]
        public ProjectUrl Url { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Project class with a unique identifier. 
        /// /// The title, description, image URL, and project URL are required parameters.
        /// </summary>
        /// <param name="title">The title of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="imageUrl">The URL of the project image.</param>
        /// <param name="projectUrl">The URL of the project.</param>
        /// <exception cref="BusinessRuleViolationException">Thrown if any of the required parameters are null or whitespace, or if the image URL or project URL is not a valid absolute URI.</exception>
        [JsonConstructor]
        public Project(string title, string description, string imageUrl, string projectUrl)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Url = new ProjectUrl(projectUrl);
        }

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
        public Project(Guid id, string title, string description, string imageUrl, string projectUrl)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Url = new ProjectUrl(projectUrl);
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
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                throw new BusinessRuleViolationException("L'URL d'image est invalide.");
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
            Url = new ProjectUrl(url);
        }
    }
}