namespace PortfolioEAI.Domain.Enums
{
    public sealed class LanguageCode : Enumeration
    {
        /// <summary>
        /// Represents the English language.
        /// </summary>
       public static readonly LanguageCode English = new(1, "English");

        /// <summary>
        /// Represents the Spanish language.
        /// </summary>
        public static readonly LanguageCode Spanish = new(2, "Spanish");

        /// <summary>
        /// Represents the French language.
        /// </summary>
        public static readonly LanguageCode French = new(3, "French");

        /// <summary>
        /// Represents the German language.
        /// </summary>
        public static readonly LanguageCode German = new(4, "German");

        /// <summary>
        /// Represents the Italian language.
        /// </summary>
        public static readonly LanguageCode Italian = new(5, "Italian");

        /// <summary>
        /// Represents the Portuguese language.
        /// </summary>
        private LanguageCode(int id, string name) : base(id, name) { }
    }
}