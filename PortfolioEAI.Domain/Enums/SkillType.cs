namespace PortfolioEAI.Domain.Enums
{
    public sealed class SkillType : Enumeration
    {
        /// <summary>
        /// Represents different types of skills in software development.
        /// </summary>
        public static readonly SkillType Frontend = new(1, "Frontend");
        /// <summary>
        /// Represents skills related to backend development.
        /// </summary>
        public static readonly SkillType Backend = new(2, "Backend");
        /// <summary>
        /// Represents skills that encompass both frontend and backend development, often referred to as fullstack development.
        /// </summary>
        public static readonly SkillType Fullstack = new(3, "Fullstack");
        /// <summary>
        /// Represents skills related to DevOps practices, which involve the integration of development and operations.
        /// </summary>
        public static readonly SkillType DevOps = new(4, "DevOps");

        /// <summary>
        /// Represents skills related to mobile application development.
        /// </summary>
        public static readonly SkillType Mobile = new(5, "Mobile");

        private SkillType(int id, string name) : base(id, name) { }
    }
}