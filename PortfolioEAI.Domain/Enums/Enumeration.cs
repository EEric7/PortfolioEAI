namespace PortfolioEAI.Domain.Enums
{
    public abstract class Enumeration : IEquatable<Enumeration>
    {
        public int Id { get; }
        public string Name { get; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public bool Equals(Enumeration? other) => other != null && Id == other.Id;

        public override bool Equals(object? obj) => obj is Enumeration other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();
    }
}