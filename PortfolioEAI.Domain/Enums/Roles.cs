namespace PortfolioEAI.Domain.Enums
{
    public sealed class Roles : Enumeration
    {
        public static readonly Roles Admin = new(1, "Admin");
        public static readonly Roles User = new(2, "User");
        public static readonly Roles Visitor = new(3, "Visitor");
        private Roles(int id, string name) : base(id, name) { }
    }
}