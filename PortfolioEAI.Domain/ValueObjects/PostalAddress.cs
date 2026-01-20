using System.Dynamic;
using System.Text.RegularExpressions;
using PortfolioEAI.Domain.Common;
using PortfolioEAI.Domain.Exceptions;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class PostalAddress
    {
        public string Street { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

#pragma warning disable CS8618 //
        private PostalAddress(){}
#pragma warning restore CS8618 //

        /// <summary>
        ///     Creates a new instance of the <see cref="PostalAddress"/> class with the specified properties.
        /// </summary>
        /// <param name="street"></param>
        /// <param name="postalCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static PostalAddress Create(string? street, string? postalCode, string? city, string? country)
        {
            var address = new PostalAddress();
            address.SetStreet(street);
            address.SetPostalCode(postalCode);
            address.SetCity(city);
            address.SetCountry(country);
            return address;
        }

        public static PostalAddress Create(string? value)
        {
            var address = new PostalAddress();
            address.SetFullAddress(value);

            return address;
        }


        /// <summary>
        ///     Sets the full address after validating it.
        /// </summary>
        /// <param name="value"></param>
        public void SetFullAddress(string? value)
        {
            var parts = ValidateFullStreet(value);

            SetStreet(parts[0]);
            SetPostalCode(parts[1]);
            SetCity(parts[2]);
            SetCountry(parts[3]);
        }

        /// <summary>
        ///     Sets the street of the address after validating it.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetStreet(string? value)
        {
            try
            {
                ValidateStreet(value);
                Street = value!;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///  Sets the city of the address after validating it.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCity(string? value)
        {
            try 
            {   
                ValidateCity(value);
                City = value!;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Sets the postal code of the address after validating it.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetPostalCode(string? value)
        {
            try
            {
                ValidatePostalCode(value);
                PostalCode = value!;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Sets the country of the address after validating it.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="BusinessRuleViolationException"></exception>
        public void SetCountry(string? value)
        {
            try
            {
                ValidateCountry(value);
                Country = value!;
            }
            catch (Exception ex)
            {
                throw new BusinessRuleViolationException(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Gets the full formatted address.
        /// </summary>
        /// <returns></returns>
        public string GetFullAddress()
        {
            return $"{Street}, {PostalCode} {City}, {Country}";
        }

        /// <summary>
        ///    Validates the provided full street address.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Format result : 'Street, PostalCode, City, Country'.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static List<string> ValidateFullStreet(string? value)
        {
            var fullAdress = Regex.Match(value!, @"^(.*),\s*(\d+)\s*([a-zA-Z\s]+),\s*(.*)$");

            if (!fullAdress.Success)
                throw new ArgumentException("The full address format is invalid. Expected format: 'Street, PostalCode City, Country'.", nameof(value));

            return fullAdress.Groups.Values.Skip(1).Select(g => g.Value).ToList();
        }

        /// <summary>
        ///     Validates the provided street.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessRuleViolationException"></exception>
        private static void ValidateStreet(string? value)
        {
             if (string.IsNullOrWhiteSpace(value))
                    throw new BusinessRuleViolationException("Street is required.", new ArgumentNullException("Street is required.", nameof(value)));

            if (value.Length < 3)
                throw new BusinessRuleViolationException("Street must be at least 3 characters.", new ArgumentException("Street must be at least 3 characters.", nameof(value)));   

            if (value.Length > 200)
                throw new BusinessRuleViolationException("Street exceeds the maximum length of 200 characters.", new ArgumentException("Street exceeds the maximum length of 200 characters.", nameof(value)));
        }

        /// <summary>
        ///     Validates the provided city.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateCity(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("City is required.", nameof(value));
            
            if (value.Length < 2)
                throw new ArgumentException("City must be at least 2 characters.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("City exceeds the maximum length of 100 characters.", nameof(value));
        }

        /// <summary>
        ///     Validates the provided postal code.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidatePostalCode(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("The postal code is required.", nameof(value));

            var postalMatch = Regex.Match(value, @"\d+");

            if (!postalMatch.Success)
                throw new ArgumentException("The postal code format is invalid.", nameof(value));
        }

        /// <summary>
        ///     Validates the provided country.
        /// </summary>
        /// <param name="value"></param>
        private static void ValidateCountry(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("The country is required.", nameof(value));

            if (value.Length < 2)
                throw new ArgumentException("Country must be at least 2 characters.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Country exceeds the maximum length of 100 characters.", nameof(value));
        }
    }
}