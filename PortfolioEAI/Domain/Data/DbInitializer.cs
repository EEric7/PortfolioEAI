using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

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
                new(new Guid(),"Portfolio Web", "Site personnel", "/images/p1.jpg", "https://github.com/..."),
                new(new Guid(),"Jeu Unity", "Mini-jeu mobile", "/images/p1.jpg", "/images/p2.jpg")
            };

            var skills = new Skill[]
            {
                new(new Guid(),"C#", SkillLevel.Advanced, SkillCategory.Fullstack),
                new(new Guid(),"JavaScript", SkillLevel.Advanced, SkillCategory.Fullstack),
                new(new Guid(),"Unity", SkillLevel.Beginner, SkillCategory.Fullstack)
            };

            var experiences = new Experience[]
            {
                new(new Guid(),"Entreprise A", "Consultant It", DateOnly.Parse("2020/1/1"), DateOnly.Parse("2021/12/31"), "Développement d'applications web", "/images/p2.jpg"),
                new(new Guid(),"Entreprise B", "Consultant", DateOnly.Parse("2021/1/1"), DateOnly.Parse("2022/12/31"), "Création de jeux mobiles", "/images/p4.jpg")
            };

            var admin = new AdminUser(new Guid(), "admin", "admin123", "elembaadi@icloud.com");

            foreach (var skill in skills)
                admin.AddSkill(skill);

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }
    }
}