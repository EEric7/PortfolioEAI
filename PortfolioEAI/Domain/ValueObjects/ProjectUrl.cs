using System.ComponentModel.DataAnnotations;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class ProjectUrl
    {
        public string Value { get; }

        public ProjectUrl(string value)
        {
#if !DEBUG
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                throw new ArgumentException("L'URL du projet est invalide.");
#endif
            Value = value;
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) =>
            obj is ProjectUrl other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator string(ProjectUrl url) => url.Value;
        public static explicit operator ProjectUrl(string value) => new(value);
    }
}