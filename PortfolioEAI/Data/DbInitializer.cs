using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // BDD déjà initialisée
            if (context.Projects.Any()) return;

            var projects = new Project[]
            {
                new(null,"Portfolio Web", "Site personnel", "/images/p1.jpg", "https://github.com/..."),
                new(null,"Jeu Unity", "Mini-jeu mobile", "/images/p1.jpg", "/images/p2.jpg")
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }
    }
}