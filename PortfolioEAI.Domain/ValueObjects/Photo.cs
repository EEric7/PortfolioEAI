using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class Photo : ValueObject
    {
        /// <summary>
        /// Nom du fichier de la photo
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        /// URL de la photo
        /// </summary>
        public string Url { get; private set; } = string.Empty;

        /// <summary>
        /// Titre ou description de la photo
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// Date de création de la photo
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.MinValue;

#pragma warning disable CS8618 //
        private Photo() {}
#pragma warning restore CS8618 //

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FileName;
            yield return Url;
            yield return Title;
            yield return CreatedAt;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Photo"/> class with the specified properties.
        /// </summary>
        /// <param name="fileName">The file name of the photo.</param>
        /// <param name="url">The URL of the photo.</param>
        /// <param name="title">The title or description of the photo.</param>
        /// <param name="createdAt">The creation date of the photo.</param>
        /// <returns>A new instance of the <see cref="Photo"/> class.</returns>
        public static Photo Create(string fileName, string url, string title, DateTime createdAt)
        {
            var photo = new Photo();
            photo.SetFileName(fileName);
            photo.SetUrl(url);
            photo.SetTitle(title);
            photo.SetCreatedAt(createdAt);
            return photo;
        }

        /// <summary>
        /// Sets the file name of the photo after validating it.
        /// </summary>
        /// <param name="value">The file name to set.</param>
        public void SetFileName(string value) 
        {
            try
            {
                FileName = ValidateFileName(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// Sets the URL of the photo after validating it.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetUrl(string value)
        {
            try
            {
                Url = ValidateUrl(value);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the title of the photo after validating it.
        /// </summary>
        /// <param name="title"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetTitle(string title)
        {
            try
            {
                Title = ValidateTitle(title);
            } 
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the creation date of the photo after validating it.
        /// </summary>
        /// <param name="createdAt"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCreatedAt(DateTime createdAt)
        {
            try
            {
                CreatedAt = ValidateCreatedAt(createdAt);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validates the provided URL.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static string ValidateUrl(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("L'URL est requise.", nameof(value));
#if !DEBUG
            if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                throw new BusinessRuleViolationException("The photo URL is invalid.", new ArgumentException(nameof(value)));
#endif   
            return value;
        }

        /// <summary>
        /// Validates the provided title.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static string ValidateTitle(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The photo title is required.", new ArgumentNullException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the provided file name.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static string ValidateFileName(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("The photo file name is required.", new ArgumentNullException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the provided creation date.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static DateTime ValidateCreatedAt(DateTime? value)
        {
            if (value == null)
                throw new BusinessRuleViolationException("The photo creation date is required.", new ArgumentNullException(nameof(value)));

            if (value > DateTime.UtcNow)
                throw new BusinessRuleViolationException("The photo creation date cannot be in the future.", new ArgumentException(nameof(value)));

            return value.Value;
        }
    }
}