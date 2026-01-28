namespace PortfolioEAI.Domain.Enums
{
    public sealed class SkillCategory : Enumeration
    {
        /// <summary>
        /// Represents no specific skill category.
        /// </summary>
        public static readonly SkillCategory None = new(0, "None");

        /// <summary>
        /// Represents skills related to frontend development, including HTML, CSS, and JavaScript.
        /// </summary>
        public static readonly SkillCategory Frontend = new(1, "Frontend");

        /// <summary>
        /// Represents skills related to backend development, including server-side programming languages and frameworks.
        /// </summary>
        public static readonly SkillCategory Backend = new(2, "Backend");

        /// <summary>
        /// Represents skills that encompass both frontend and backend development, often referred to as fullstack development.
        /// </summary>
        public static readonly SkillCategory Fullstack = new(3, "Fullstack");

        /// <summary>
        /// Represents skills related to DevOps practices, which involve the integration of development and operations.
        /// </summary>
        public static readonly SkillCategory DevOps = new(4, "DevOps");

        /// <summary>
        /// Represents skills related to database management, including SQL and NoSQL databases.
        /// </summary>
        public static readonly SkillCategory Framework = new(5, "Framework");

        /// <summary>
        /// Represents skills related to programming languages, such as Python, Java, C#, etc.
        /// </summary>
        public static readonly SkillCategory Languages = new(6, "Languages");

        /// <summary>
        /// Represents skills related to mobile application development.
        /// </summary>
        public static readonly SkillCategory Mobile = new(7, "Mobile");

        /// <summary>
        /// Represents skills related to design, including UI/UX design, graphic design, and other creative disciplines.
        /// </summary>
        public static readonly SkillCategory Design = new(8, "Design");
        
        private SkillCategory(int id, string name) : base(id, name) { }
    }
}