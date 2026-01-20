using System.Security.Cryptography;
using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32; 
        private const int Iterations = 100_000;

        public string HashValue { get; private set; } = default!;

#pragma warning disable CS8618 
        private Password() { } // EF Core
#pragma warning restore CS8618 

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return HashValue;
        }

        /// <summary>
        /// Creates a new Password value object with a hashed password using PBKDF2.
        /// </summary>
        /// <param name="value">The plain text password to hash.</param>
        /// <returns>A new Password instance with the hashed password.</returns>
        /// <exception cref="ArgumentException">Thrown when password doesn't meet requirements.</exception>
        public static Password Create(string? value)
        {
            var password = new Password();
            password.SetPassword(value);
            return password;
        }

        /// <summary>
        /// Updates the password with a new plain text password after validation.
        /// </summary>
        /// <param name="value">The new plain text password.</param>
        public void SetPassword(string? value)
        {
            try
            {
                // Validate the password
                string password = Validate(value);

                // Generate salt and hash the password
                using var algorithm = new Rfc2898DeriveBytes(password,SaltSize,Iterations,HashAlgorithmName.SHA256);
                var salt = algorithm.Salt;
                var key = algorithm.GetBytes(KeySize);
                var hashBytes = Combine(salt, key);
                var hash = Convert.ToBase64String(hashBytes);

                // Set the hashed password
                HashValue = hash;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Verifies if a plain text password matches the stored hash.
        /// </summary>
        /// <param name="value">The plain text password to verify.</param>
        /// <returns>True if the password matches, false otherwise.</returns>
        public bool Verify(string value)
        {
            try
            {
                // Extract salt and stored key from the hash
                var hashBytes = Convert.FromBase64String(HashValue);
                var salt = hashBytes[..SaltSize];
                var storedKey = hashBytes[SaltSize..];
                using var algorithm = new Rfc2898DeriveBytes(value,salt,Iterations,HashAlgorithmName.SHA256);
                var computedKey = algorithm.GetBytes(KeySize);
                return CryptographicOperations.FixedTimeEquals(storedKey, computedKey);
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Combines salt and key into a single byte array.
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] Combine(byte[] salt, byte[] key)
        {
            var result = new byte[salt.Length + key.Length];
            Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
            Buffer.BlockCopy(key, 0, result, salt.Length, key.Length);
            return result;
        }

        /// <summary>
        /// Validates the provided password against defined rules.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string Validate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleViolationException("Password is required.", new ArgumentNullException(nameof(value)));

            if (value.Length < 8)
                throw new BusinessRuleViolationException("Password must be at least 8 characters.", new ArgumentException(nameof(value)));
                
            if (!value.Any(char.IsUpper))
                throw new BusinessRuleViolationException("Password must contain an uppercase letter.", new ArgumentException(nameof(value)));

            if (!value.Any(char.IsLower))
                throw new BusinessRuleViolationException("Password must contain a lowercase letter.", new ArgumentException(nameof(value)));

            if (!value.Any(char.IsDigit))
                throw new BusinessRuleViolationException("Password must contain a digit.", new ArgumentException(nameof(value)));
            
            return value;
        }
    }
}