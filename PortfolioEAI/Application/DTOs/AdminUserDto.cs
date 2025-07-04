
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
        /// Name of the admin user.
        /// This property represents the name of the admin user.
        /// It is typically used for display purposes in user interfaces.
        /// The name can be a full name or a username, depending on the application's requirements.
        /// </summary>
        public string? Name { get; set; }

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
        /// Date and time when the admin user was created.
        /// This property represents the timestamp of when the admin user account was created in the system.
        /// It is typically used for auditing purposes and to track the history of user accounts.
        /// The value is usually set automatically when the account is created and can be used to determine
        /// how long the account has been active.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the admin user was last updated.
        /// This property represents the timestamp of the last update made to the admin user account.
        /// It is useful for tracking changes to user information, such as updates to the name,
        /// email, password, or role. This value is typically set automatically whenever the user account
        /// is modified, allowing administrators to see when the last change occurred.
        public DateTime UpdatedAt { get; set; }
    }
}