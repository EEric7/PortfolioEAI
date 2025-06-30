using PortfolioEAI.Models;

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
                new Project { Title = "Portfolio Web", Description = "Site personnel", ImageUrl = "/images/p1.jpg", ProjectUrl = "https://github.com/..." },
                new Project { Title = "Jeu Unity", Description = "Mini-jeu mobile", ImageUrl = "/images/p2.jpg", ProjectUrl = "https://github.com/..." }
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }
    }
}