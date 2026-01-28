namespace PortfolioEAI.Domain.Enums
{
    public sealed class SkillLevel : Enumeration
    {
        /// <summary>
        /// Represents no skill level specified.
        /// </summary>
        public static readonly SkillLevel None = new(0, "None");

        /// <summary>
        /// Represents a beginner skill level.
        /// </summary>
        public static readonly SkillLevel Beginner = new(25, "Beginner");

        /// <summary>
        /// Represents an intermediate skill level.
        /// </summary>
        public static readonly SkillLevel Intermediate = new(50, "Intermediate");

        /// <summary>
        /// Represents an advanced skill level.
        /// </summary>
        public static readonly SkillLevel Advanced = new(75, "Advanced");

        /// <summary>
        /// Represents an expert skill level.
        /// </summary>
        public static readonly SkillLevel Expert = new(100, "Expert");

        private SkillLevel(int id, string name) : base(id, name) { }
    }
}