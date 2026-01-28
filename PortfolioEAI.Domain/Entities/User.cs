using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Enums;
using PortfolioEAI.Domain.Exceptions;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Domain.Entities
{
    public class User : AggregateRoot
    {
        private readonly List<Skill> _skills = [];
        private readonly List<Experience> _experiences  = [];

        public string? Firstname { get; private set; } = string.Empty;
        public string? Lastname { get; private set; } = string.Empty;
        public string? DisplayName { get; private set; } = default;
        public string? Description { get; private set; } = string.Empty;
        public string Profession { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public StoredFile? ProfilePhoto { get; private set; } = default;
        public Roles Role { get; private set; } = Roles.Visitor;
        public PostalAddress? Address { get; private set; } = default;
        public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();       
        public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();

#pragma warning disable CS8618 
        private User() { } // EF Core
#pragma warning restore CS8618 

        /// <summary>
        /// Creates a new User instance with the specified email and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Create(string? email, string? password)
        {
            var user = new User();
            user.Email.SetEmail(email);
            user.Password.SetPassword(password);
            return user;
        }
        
        /// <summary>
        /// Sets the firstname of the admin user.
        /// This method allows you to set or update the firstname of the admin user.
        /// It is important to ensure that the firstname being set is not null and meets length requirements.
        /// If the firstname is null, a BusinessRuleViolationException will be thrown.
        /// If the firstname exceeds 50 characters, a BusinessRuleViolationException will also be thrown.
        /// If the firstname contains invalid characters, a BusinessRuleViolationException will also be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetFirstname(string? value)
        {
            try
            {
                Firstname = ValidateFirstname(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the lastname of the admin user.
        /// This method allows you to set or update the lastname of the admin user.
        /// It is important to ensure that the lastname being set is not null and meets length requirements.
        /// If the lastname is null, a BusinessRuleViolationException will be thrown.
        /// If the lastname exceeds 50 characters, a BusinessRuleViolationException will also be thrown.
        /// If the lastname contains invalid characters, a BusinessRuleViolationException will also be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetLastname(string? value)
        {
            try
            {
                Lastname = ValidateLastname(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

       /// <summary>
       ///  Sets the display name of the admin user. 
       /// </summary>
       /// <param name="value"></param>
       /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetDisplayName(string? value)
        {
            try
            {
                DisplayName = ValidateDisplayName(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the description of the admin user.
        /// This method allows you to set or update the description of the admin user.
        /// It is important to ensure that the description being set is not null and meets length requirements.
        /// If the description is null, a BusinessRuleViolationException will be thrown.
        /// If the description exceeds 500 characters, a BusinessRuleViolationException will also be thrown
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetDescription(string? value)
        {
            try
            {
                Description = ValidateDescription(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        public void SetProfession(string? value)
        {
            try
            {
                Profession = ValidateProfession(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the email address of the admin user.
        /// This method allows you to set or update the email address of the admin user.
        /// It is important to ensure that the email being set is not null and meets format requirements.
        /// If the email is null, a BusinessRuleViolationException will be thrown.
        /// If the email format is invalid, a BusinessRuleViolationException will also be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetEmail(string? value)
        {
            Email.SetEmail(value);
        }

        /// <summary>
        /// Sets the password of the admin user.
        /// This method allows you to set or update the password of the admin user.
        /// It is important to ensure that the password being set is not null and meets security requirements.
        /// If the password is null, a BusinessRuleViolationException will be thrown.
        /// If the password does not meet security requirements, a BusinessRuleViolationException will also be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetPassword(string? value)
        {
            Password.SetPassword(value);
        }

        /// <summary>
        /// Sets the address of the admin user.
        /// This method allows you to set or update the postal address of the admin user.
        /// It is important to ensure that the address being set is not null and meets format requirements.
        /// If the address is null, a BusinessRuleViolationException will be thrown.
        /// If the address format is invalid, a BusinessRuleViolationException will also be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetAddress(string? value)
        {
            if (Address is null)
                Address = PostalAddress.Create(value);
            else
                Address.SetFullAddress(value);
        }

        /// <summary>
        /// Sets the profile photo of the admin user.
        /// This method allows you to set or update the profile photo of the admin user.
        /// If the photo is null, the profile photo will be set to null.
        /// If a valid photo is provided, it will be assigned to the ProfilePhoto property.
        /// </summary>
        /// <param name="photo"></param>
        public void SetProfilePhoto(StoredFile? photo)
        {
            if (photo is null)
                ProfilePhoto = null;
            else
                ProfilePhoto = photo;
        }

        /// <summary>
        ///     Sets the roles of the admin user.
        ///     This method allows you to set or update the roles assigned to the admin user.
        ///     It clears any existing roles and adds the provided roles to the user's role collection.
        /// </summary>
        /// <param name="roles"></param>
        public void SetRole(string? value)
        {
            try
            {
                Role = ValidateRole(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///    Sets the skills of the admin user.
        ///    This method allows you to set or update the skills assigned to the admin user.
        ///    It clears any existing skills and adds the provided skills to the user's skill collection.
        /// </summary>
        /// <param name="skills"></param>
        public void SetSkills(IEnumerable<Skill> skills)
        {    
            _skills.Clear();
            foreach (var skill in skills)
                AddSkill(skill);
        }
        /// <summary>
        ///    Sets the experiences of the admin user.
        ///    This method allows you to set or update the experiences assigned to the admin user.
        ///    It clears any existing experiences and adds the provided experiences to the user's experience collection.
        /// </summary>
        /// <param name="experiences"></param>
        public void SetExperiences(IEnumerable<Experience> experiences)
        {
            _experiences.Clear();
            foreach (var experience in experiences)
                AddExperience(experience);
        }

        /// <summary>
        /// Adds a skill to the admin user.
        /// This method allows you to add a new skill to the admin user's profile.
        /// It is important to ensure that the skill being added is not null.
        /// If the skill is null, a BusinessRuleViolationException will be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public bool AddSkill(Skill value)
        {
            try
            {
                bool exists = _skills.Any(s => s.Id == value.Id || s.Name == value.Name);

                if (exists)
                    return false;

                _skills.Add(value);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// Removes a skill from the admin user.
        /// This method allows you to remove an existing skill from the admin user's profile.
        /// It is important to ensure that the skill being removed is not null.
        /// If the skill is null, a BusinessRuleViolationException will be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void RemoveSkill(Skill value)
        {
            try
            {
                bool exists = _skills.Any(s => s.Id == value.Id);

                if (!exists)
                    return;

                _skills.Remove(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Removes multiple skills from the admin user.
        /// </summary>
        /// <param name="values"></param>
        public void RemoveSkill(IEnumerable<Skill> values)
        {
            foreach (var value in values)
            {
                RemoveSkill(value);
            }
        }

        /// <summary>
        /// Adds an experience to the admin user.
        /// This method allows you to add a new experience to the admin user's profile.
        /// It is important to ensure that the experience being added is not null.
        /// If the experience is null, a BusinessRuleViolationException will be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public bool AddExperience(Experience value)
        {
            try
            {
                bool exists = _experiences.Any(e => e.Id == value.Id && e.Company == value.Company);

                if (exists)
                    return false;

                _experiences.Add(value);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Adds a project to a specific experience of the admin user.
        /// </summary>
        /// <param name="expID"></param>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public bool AddProject(Guid expID, Project value)
        {
            try
            {
                int index = _experiences.FindIndex(e => e.Id == expID);

                if (index == -1)
                    return false;

                bool exists = _experiences[index].Projects.Any(p => p.Id == value.Id && p.Title == value.Title && p.Description == value.Description);

                if (exists)
                    return false;

                return _experiences[index].AddProject(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Removes an experience from the admin user.
        /// This method allows you to remove an existing experience from the admin user's profile.
        /// It is important to ensure that the experience being removed is not null.
        /// If the experience is null, a BusinessRuleViolationException will be thrown.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void RemoveExperience(Experience value)
        {
            try
            {
                bool existing = _experiences.Any(e => e.Id == value.Id);

                if (!existing)
                    return;

                _experiences.Remove(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Removes multiple experiences from the admin user.
        /// </summary>
        /// <param name="values"></param>
        public void RemoveExperience(IEnumerable<Experience> values)
        {
            foreach (var value in values)
            {
                RemoveExperience(value);
            }
        }

        /// <summary>
        /// Validates the firstname of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        private static string ValidateFirstname (string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Firstname is required.", new ArgumentNullException(nameof(value)));
            
            if (!value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\''))
                throw new BusinessRuleViolationException("Firstname contains invalid characters.", new ArgumentException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the lastname of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static string ValidateLastname (string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Lastname is required.", new ArgumentNullException(nameof(value)));
            
            if (!value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\''))
                throw new BusinessRuleViolationException("Lastname contains invalid characters.", new ArgumentException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the display name of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static string ValidateDisplayName (string? value)
        {

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '_' || c == '-'))
                    throw new BusinessRuleViolationException("Display name contains invalid characters.", new ArgumentException(nameof(value)));
            }

            return value?? string.Empty;
        }

        /// <summary>
        /// Validates the role of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static Roles ValidateRole(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Role is required.", new ArgumentNullException(nameof(value)));

            return value switch
            {
                "Admin" => Roles.Admin,
                "User" => Roles.User,
                "Visitor" => Roles.Visitor,
                _ => throw new BusinessRuleViolationException("Invalid role.", new ArgumentException(nameof(value))),
            };
        }

        /// <summary>
        /// Validates the description of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static string ValidateDescription (string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Description is required.", new ArgumentNullException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the profession of the user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ValidateProfession (string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Profession is required.", new ArgumentNullException(nameof(value)));

            return value;
        }
    }
}