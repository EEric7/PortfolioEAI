using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Domain.Entities
{
    public class AdminUser
    {
        /// <summary>
        /// Gets or sets the unique identifier for the admin user.
        /// This property represents the unique identifier for the admin user.
        /// It is a required field and should be a valid GUID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the admin user.
        /// This property represents the unique identifier for the admin user.
        /// It is a required field and should be unique across all admin users.
        /// </summary>
        public string? Username { get; private set; }

        /// <summary>
        /// Gets or sets the password of the admin user.
        /// This property represents the password used for authentication.
        /// It is a required field and should be kept secure.
        /// The password should be stored in a secure manner, such as hashed and salted,
        /// to protect against unauthorized access.
        /// It is important to ensure that the password meets security requirements,
        /// such as minimum length and complexity, to enhance the security of the admin user account.
        /// </summary>
        public string? Password { get; private set; }

        /// <summary>
        /// Gets or sets the email address of the admin user.
        /// /// This property represents the email address associated with the admin user.
        /// It is a required field and should be a valid email format.
        /// The email address is used for communication purposes, such as sending notifications or password reset links.
        /// It is important to ensure that the email address is well-formed and valid to avoid
        /// issues with email delivery or user authentication.
        /// The email address should be unique across all admin users to prevent conflicts.
        /// It is typically used in scenarios where the admin user needs to receive important information or updates
        /// related to their account or the system they are managing.
        /// The email address is stored as a value object of type <see cref="Email"/>
        /// which encapsulates the validation logic for email addresses.
        /// </summary>
        
        public Email Email { get; private set; }

 
        public string? Description { get; private set; }

        /// <summary>
        /// Gets or sets the list of skills associated with the admin user.
        /// This property represents the skills that the admin user possesses.
        /// It is a collection of <see cref="Skill"/> objects, allowing the admin user
        /// to have multiple skills associated with their profile.
        /// </summary>
        public IList<Skill> Skills { get; set; } = new List<Skill>();

        /// <summary>
        /// Gets or sets the list of experiences associated with the admin user.
        /// </summary>
        public IList<Experience> Experiences { get; set; } = new List<Experience>();

        // Default constructor for EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AdminUser() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the AdminUser class with the specified parameters.
        /// This constructor allows you to create an admin user with a specific ID, username, password, and email address.
        /// It is typically used when creating a new admin user in the system.
        /// The ID is a unique identifier for the admin user, while the username, password,
        /// and email address are used for authentication and communication purposes.
        /// The username should be unique across all admin users, and the password should be stored securely
        /// (e.g., hashed and salted) to protect against unauthorized access.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        public AdminUser(Guid id, string username, string password, string email, string description,IList<Skill> skills, IList<Experience> experiences)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = new Email(email);
            Description = description;
            Skills = skills ?? new List<Skill>();
            Experiences = experiences ?? new List<Experience>();
        }

        /// <summary>
        /// Sets the username of the admin user.
        /// This method allows you to change the username of the admin user to a new value.
        /// It is important to ensure that the username being set is not null or empty.
        /// If the username is null or empty, a BusinessRuleViolationException will be thrown.
        /// The username will be stored in the Username property of the admin user.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        /// <exception cref="ArgumentNullException">Thrown when the username is null.</exception>
        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new BusinessRuleViolationException("The company name is required.", new ArgumentNullException(nameof(username)));
            Username = username;
        }

        /// <summary>
        /// Sets the password of the admin user.
        /// This method allows you to change the password of the admin user to a new value.
        /// It is important to ensure that the password being set is not null or empty.
        /// If the password is null or empty, a BusinessRuleViolationException will be thrown.
        /// The password will be stored in the Password property of the admin user.
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        /// <exception cref="ArgumentNullException">Thrown when the password is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the password is not well-formed.</exception>
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessRuleViolationException("The company name is required.", new ArgumentNullException(nameof(password)));
            Password = password;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessRuleViolationException("The description is required.", new ArgumentNullException(nameof(description)));
            Description = description;
        }

        /// <summary>
        /// Sets the email address of the admin user.
        /// </summary>
        public void SetEmail(string email)
        {
            Email = new Email(email);
        }
        
        /// <summary>
        /// Adds a skill to the admin user.
        /// This method allows you to add a new skill to the admin user's profile.
        /// It is important to ensure that the skill being added is not null.
        /// If the skill is null, a BusinessRuleViolationException will be thrown.
        /// The skill will be added to the Skills collection of the admin user.
        /// </summary>
        /// <param name="skill"></param>
        /// <exception cref="BusinessRuleViolationException">Thrown when the skill is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the skill is null.</exception>
        public void AddSkill(Skill skill)
        {
            if (skill == null)
                throw new BusinessRuleViolationException("Skill cannot be null.", new ArgumentNullException(nameof(skill)));
            
            if (Skills.Contains(skill))
                throw new BusinessRuleViolationException("Skill already exists in the admin user's skills.", new Exception(nameof(skill)));

            Skills.Add(skill);
        }
        
        /// <summary>
        /// Removes a skill from the admin user.
        /// This method allows you to remove an existing skill from the admin user's profile.
        /// It is important to ensure that the skill being removed is not null.
        /// If the skill is null, a BusinessRuleViolationException will be thrown.
        /// The skill will be removed from the Skills collection of the admin user.
        /// </summary>
        /// <param name="skill"></param>
        /// <exception cref="BusinessRuleViolationException">Thrown when the skill is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the skill is null.</exception>
        public void RemoveSkill(Skill skill)
        {
            if (skill == null)
                throw new BusinessRuleViolationException("Skill cannot be null.", new ArgumentNullException(nameof(skill)));

            if (!Skills.Contains(skill))
                throw new BusinessRuleViolationException("Skill not found in the admin user's skills.", new Exception(nameof(skill)));

            Skills.Remove(skill);
        }

        public void AddExperience(Experience experience)
        {
            if (experience == null)
                throw new BusinessRuleViolationException("Experience cannot be null.", new ArgumentNullException(nameof(experience)));

            if (Experiences.Contains(experience))
                throw new BusinessRuleViolationException("Experience already exists in the admin user's experiences.", new Exception(nameof(experience)));

            Experiences.Add(experience);
        }

        public void RemoveExperience(Experience experience)
        {
            if (experience == null)
                throw new BusinessRuleViolationException("Experience cannot be null.", new ArgumentNullException(nameof(experience)));

            if (!Experiences.Contains(experience))
                throw new BusinessRuleViolationException("Experience not found in the admin user's experiences.", new Exception(nameof(experience)));

            Experiences.Remove(experience);
        }
        
        /// <summary>
        /// Returns a string representation of the AdminUser object.
        /// This method provides a human-readable representation of the admin user's details,
        /// including the username and email address.
        /// It is useful for debugging or logging purposes to quickly identify the admin user.
        /// </summary>
        /// <returns>A string representation of the AdminUser object.</returns>
        public override string ToString()
        {
            return $"AdminUser: {Username}, Email: {Email}";
        }
    }
}