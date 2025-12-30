using System.Text.RegularExpressions;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class Email : IEquatable<Email>
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
        public string Value { get; private set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// This constructor takes a string value representing the email address.
        /// It validates the email address to ensure it is well-formed.
        /// If the email address is not valid, it throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="value">The email address to be encapsulated by this value object.</param>
        /// <exception cref="ArgumentException">Thrown when the email address is null, empty, or not well-formed.</exception>
        public Email(string? value)
        {
            SetValue(value);
        }

        public void SetValue(string? value)
        {
#if !DEBUG
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("L'adresse e-mail est requise.", new ArgumentNullException(nameof(value)));      

            if (!EmailRegex.IsMatch(value))
                throw new BusinessRuleViolationException("L'adresse e-mail n'est pas valide.", new ArgumentNullException(nameof(value)));
#endif
            Value = (value ?? string.Empty).Trim().ToLowerInvariant();
        }

        public override string ToString() => Value;

        public bool Equals(Email? other) => other is not null && Value == other.Value;

        public override bool Equals(object? obj) =>
            obj is Email other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Email left, Email right) => left.Equals(right);
        public static bool operator !=(Email left, Email right) => !left.Equals(right);

        // Conversion implicite → string
        public static implicit operator string(Email email) => email.Value;

        // Conversion explicite ← string
        public static explicit operator Email(string value) => new Email(value);
    }
}