
namespace PortfolioEAI.Application.DTOs
{
    public class AdminUserDto
    {
        /// <summary>
        /// Unique identifier for the admin user.
        /// This property is used to uniquely identify an admin user in the system.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// UserName of the admin user.
        /// This property represents the name of the admin user.
        /// It is typically used for display purposes in user interfaces.
        /// The name can be a full name or a username, depending on the application's requirements.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Email address of the admin user.
        /// This property holds the email address associated with the admin user.
        /// It is used for communication purposes, such as sending notifications or password reset links.
        /// The email should be a valid format and unique within the system.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Password for the admin user.
        /// This property stores the password for the admin user.
        /// It is typically hashed and stored securely to protect user credentials.
        /// The password should meet security requirements, such as minimum length and complexity.
        /// It is important to handle passwords securely to prevent unauthorized access to the admin account.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Role of the admin user.
        /// This property indicates the role or permissions assigned to the admin user.
        /// It is used to determine the level of access and functionality available to the user within the application.
        /// Common roles might include "Admin", "Editor", "Viewer", etc.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        ///  Description of the admin user.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the postal address of the admin user.
        /// This property represents the postal address associated with the admin user.
        /// It is an optional field and can be null if the admin user does not have a postal address.
        /// The postal address is stored as a value object of type <see cref="PostalAddressDto"/>
        /// which encapsulates the details of the address, such as street, city, postal code, and country.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Indicates whether the admin user is active.
        /// This property is a boolean value that specifies if the admin user account is currently active or not.
        /// An active user can log in and perform actions, while an inactive user may be restricted from accessing the system.
        /// This is useful for managing user accounts, especially in scenarios where accounts need to be temporarily disabled or deleted.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// List of skills associated with the admin user.
        /// This property holds a collection of skills that the admin user possesses.
        /// </summary>
        public IList<SkillDto> Skills { get; set; } = new List<SkillDto>();

        /// <summary>
        /// List of experiences associated with the admin user.
        /// </summary>
        public IList<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();

        public AdminUserDto() { }

        public AdminUserDto(AdminUserDto dto)
        {
            Id = dto.Id;
            UserName = dto.UserName;
            Email = dto.Email;
            Password = dto.Password;
            Role = dto.Role;
            Description = dto.Description;
            Address = dto.Address;
            IsActive = dto.IsActive;
            Skills = dto.Skills;
            Experiences = dto.Experiences;
        }
    }
}