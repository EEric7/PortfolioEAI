using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortfolioEAI.Web.Domain.ValueObjects
{
    public class PostalAddress
    {
        public string Street { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

        public PostalAddress(string street, string city, string postalCode, string country)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        public PostalAddress(string? fullAddress)
        {
            var parts = fullAddress?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            
            if (parts.Length >= 1)
                Street = parts[0].Trim();
            
            if (parts.Length >= 2)
            {
                // Extraire le code postal (numéros uniquement)
                var postalMatch = Regex.Match(parts[1], @"\d+");
                if (postalMatch.Success)
                    PostalCode = postalMatch.Value;
                
                // Extraire la ville (texte sans numéros)
                City = Regex.Replace(parts[1], @"\d+", "").Trim();
            }
            
            if (parts.Length >= 3)
                Country = parts[2].Trim();
        }

        public string GetFullAddress()
        {
            return $"{Street}, {PostalCode} {City}, {Country}";
        }
    }
}