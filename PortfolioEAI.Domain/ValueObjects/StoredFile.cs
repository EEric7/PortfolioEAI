using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class StoredFile : ValueObject
    {
        /// <summary>
        /// Taille maximale d'une photo en octets (10 MB)
        /// </summary>
        private const long maxSize = 10 * 1024 * 1024;

        /// <summary>
        /// Nom du fichier de la photo
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// URL de la photo
        /// </summary>
        public string Type { get; private set; } = string.Empty;

        /// <summary>
        /// Titre ou description de la photo
        /// </summary>
        public long Size { get; private set; } = 0;

        /// <summary>
        /// Date de création de la photo
        /// </summary>
        public byte[] Content { get; private set; } = default!; 

        /// <summary>
        /// Date et heure de l'upload de la photo
        /// </summary>
        public DateTimeOffset UploadedAt { get; private set; } = DateTimeOffset.UtcNow;

#pragma warning disable CS8618 //
        private StoredFile() {}
#pragma warning restore CS8618 //

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
            yield return Size;
            yield return Content;
            yield return UploadedAt;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StoredFile"/> class with the specified properties.
        /// </summary>
        /// <param name="fileName">The file name of the photo.</param>
        /// <param name="url">The URL of the photo.</param>
        /// <param name="title">The title or description of the photo.</param>
        /// <param name="createdAt">The creation date of the photo.</param>
        /// <returns>A new instance of the <see cref="StoredFile"/> class.</returns>
        public static StoredFile Create(string? fileName, string? type, long? size, byte[]? content, DateTimeOffset? createdAt)
        {
            var photo = new StoredFile();
            photo.SetFileName(fileName);
            photo.SetType(type);
            photo.SetSize(size);
            photo.SetContent(content);
            photo.SetUploadedAt(createdAt);
            return photo;
        }

        /// <summary>
        /// Sets the file name of the photo after validating it.
        /// </summary>
        /// <param name="value">The file name to set.</param>
        public void SetFileName(string? value) 
        {
            try
            {
                Name = Path.GetFileName(ValidateFileName(value));
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
        public void SetType(string? value)
        {
            try
            {
                Type = ValidateType(value);
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
        public void SetSize(long? size)
        {
            try
            {
                Size = ValidateSize(size);
            } 
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        public void SetContent(byte[]? content)
        {
            try
            {
                Content = ValidateContent(content);
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
        public void SetUploadedAt(DateTimeOffset? uploadedAt)
        {
            try
            {
                UploadedAt = ValidateUploadedAt(uploadedAt);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
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
        /// Validates the provided Type.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static string ValidateType(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? "application/octet-stream" : value;
        }

        /// <summary>
        /// Validates the provided title.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static long ValidateSize(long? value)
        {   
            if (value > maxSize)
                throw new BusinessRuleViolationException("The photo size must be less than or equal to 10 MB.", new ArgumentOutOfRangeException(nameof(value)));
        
            if (value <= 0)
                throw new BusinessRuleViolationException("The photo size must be greater than zero.", new ArgumentOutOfRangeException(nameof(value)));

            return value!.Value;
        }

        /// <summary>
        ///     Validates the provided content.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private byte[] ValidateContent(byte[]? value)
        {
            if (value == null || value.Length == 0)
                throw new BusinessRuleViolationException("The photo content is required.", new ArgumentNullException(nameof(value)));

            return value;
        }

        /// <summary>
        /// Validates the provided upload date.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static DateTimeOffset ValidateUploadedAt(DateTimeOffset? value)
        {
            if (value == null)
                throw new BusinessRuleViolationException("The photo upload date is required.", new ArgumentNullException(nameof(value)));

            if (value > DateTimeOffset.UtcNow)
                throw new BusinessRuleViolationException("The photo upload date cannot be in the future.", new ArgumentException(nameof(value)));

            return value.Value;
        }
    }
}