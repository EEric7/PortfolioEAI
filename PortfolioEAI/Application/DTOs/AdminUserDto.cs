
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
        public IList<Guid> Skills { get; set; } = new List<Guid>();
    }
}