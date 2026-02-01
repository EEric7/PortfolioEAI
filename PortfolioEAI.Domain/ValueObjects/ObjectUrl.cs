using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class ObjectUrl : ValueObject
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
#pragma warning disable CS8618 //
        private ObjectUrl() {}
#pragma warning restore CS8618 //

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="ObjectUrl"/> class with the specified URL.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ObjectUrl Create(string value)
        {
            var objectUrl = new ObjectUrl();
            objectUrl.SetValue(value);
            return objectUrl;
        }

        /// <summary>
        /// Sets the URL value after validating its format.
        /// This method checks if the provided URL is a well-formed absolute URI.
        /// If the URL is invalid, it throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string? value)
        {
            try
            {
                Value = Validate(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Validates the provided URL.
        /// </summary>
        /// <param name="value"></param>
        private static string Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The project URL is required.", new ArgumentNullException(nameof(value)));

            if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                throw new BusinessRuleViolationException("The project URL is invalid.", new ArgumentException(nameof(value)));
                
            return value;
        }
    }
}