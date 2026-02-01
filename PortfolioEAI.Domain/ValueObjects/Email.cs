using System.Text.RegularExpressions;
using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        /// <summary>
        /// Represents an email address.
        /// This value object encapsulates the email address, ensuring it is well-formed and valid.
        /// It is used to provide a consistent way to handle email addresses throughout the application.
        /// The email address is stored as a string and can be implicitly converted to and from a string.
        /// It is important that the email address is valid, and it is validated upon instantiation.
        /// If the email address is not well-formed, an exception will be thrown.
        /// </summary>
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Gets the email address value.
        /// This property holds the actual email address as a string.
        /// It is set during the construction of the Email object and is validated to ensure it meets the required format.
        /// </summary>
        public string Value {get; private set;} = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// This constructor is private to enforce the use of the factory method for creating instances.
        /// </summary>
#pragma warning disable CS8618 //
        private Email(){}
#pragma warning restore CS8618 //

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Email"/> class with the specified email address.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Email Create(string? value)
        {
            var email = new Email();
            email.SetEmail(value);
            return email;
        }

        /// <summary>
        /// Sets the email address value after validating its format.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetEmail(string? value)
        {
            try
            {
                if (Value != value)
                    Value = ValidateEmail(value).Trim().ToLowerInvariant();
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validates the provided email address.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static string ValidateEmail(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                    throw new BusinessRuleViolationException("The email is required.", new ArgumentNullException(nameof(value)));

            if (value.Length > 254)
                throw new  BusinessRuleViolationException("The email exceeds the maximum length of 254 characters.",new ArgumentException(nameof(value)));
                
            if (!EmailRegex.IsMatch(value))
                throw new BusinessRuleViolationException("The email format is invalid.", new ArgumentException(nameof(value)));

            return value;
        }
    }
}