
using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Enums;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class Role : ValueObject
    {
        public Roles Name { get; private set; } = default!;

#pragma warning disable CS8618 //
        private Role() { } // EF
#pragma warning restore CS8618 //

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
        }

        /// <summary>
        ///    Creates a new instance of the <see cref="Role"/> class with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Role Create (string name)
        {
            Role role = new();
            role.SetName(name);
            return role;
        }

        /// <summary>
        ///    Sets the role name after validating it.
        /// </summary>
        /// <param name="value"></param>
        public void SetName(string value)
        {
            try
            {
                Name = Validate(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///    Validates the provided role name.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private static Roles Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Role name is required.", nameof(value));

            if (!Enum.TryParse<Roles>(value, out var  name))
                    throw new ArgumentException($"Le rôle '{value}' n'est pas valide.", nameof(value));

            return name;
        }
    }
}