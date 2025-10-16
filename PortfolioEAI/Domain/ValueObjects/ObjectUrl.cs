using System.ComponentModel.DataAnnotations;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class ObjectUrl
    {
        /// <summary>
        /// Represents the URL of a project.
        /// This value object encapsulates the URL of a project, ensuring that it is well-formed and valid.
        /// It is used to provide a consistent way to handle project URLs throughout the application.
        /// The URL is stored as a string and can be implicitly converted to and from a string.
        /// It is important that the URL is an absolute URI, and it is validated upon instantiation.
        /// If the URL is not well-formed, an exception will be thrown.
        /// </summary>
        public string Value { get; private set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectUrl"/> class.
        /// This constructor takes a string value representing the project URL.
        /// It validates the URL to ensure it is well-formed.
        /// If the URL is not valid, it throws an <see cref="ArgumentException"/>.
        public ObjectUrl(string value)
        {
            SetValue(value);
        }

        public void SetValue(string value)
        {
#if !DEBUG
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                throw new ArgumentException("L'URL est invalide.");
#endif
            Value = value;
        }

        /// <summary>
        /// Returns a string representation of the project URL.
        /// This method overrides the default ToString() method to return the URL value.
        /// It is useful for displaying the URL in user interfaces or logging.
        /// </summary>
        public override string ToString() => Value;

        /// <summary>
        /// Determines whether the specified object is equal to the current project URL.
        /// This method overrides the default Equals method to compare the URL values.
        /// It returns true if the specified object is a <see cref="ObjectUrl"/> and
        public override bool Equals(object? obj) =>
            obj is ObjectUrl other && Value == other.Value;
        
        /// <summary>
        /// Returns a hash code for the current project URL.
        /// This method overrides the default GetHashCode method to return the hash code of the URL value.
        /// It is used in collections and other data structures to efficiently compare project URLs.
        /// </summary>
        /// <returns>A hash code for the current project URL.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator string(ObjectUrl url) => url.Value;
        public static explicit operator ObjectUrl(string value) => new(value);
    }
}