using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Assure que la base de données et le schéma existent
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (environment)
            {
                case "Development":

                    if (context.Database.EnsureCreated())
                        context.Database.Migrate();

                    context.AdminUsers.RemoveRange(context.AdminUsers);
                    context.Skills.RemoveRange(context.Skills);
                    context.Experiences.RemoveRange(context.Experiences);
                    context.Projects.RemoveRange(context.Projects);
                    
                    AddPlayGroundData(context);
                    break;
                case "Staging":
                    // Logique pour l'environnement de staging
                    break;
                case "Production":
                    // Logique pour l'environnement de production
                    break;
                default:
                    throw new InvalidOperationException($"Unknown environment: {environment}");
            }
        }

        private static void AddPlayGroundData(ApplicationDbContext context)
        {
            var admin = new AdminUser(new Guid(), "Admin", "admin321", "elembaadi@icloud.com");

            var skills = new Skill[]
            {
                new(new Guid(),"C#", SkillLevel.Advanced, SkillCategory.Fullstack),
                new(new Guid(),"JavaScript", SkillLevel.Advanced, SkillCategory.Fullstack),
                new(new Guid(),"Unity", SkillLevel.Beginner, SkillCategory.Fullstack)
            };

            foreach (var skill in skills)
                admin.AddSkill(skill);

            var experiences = new Experience[]
            {
                new(new Guid(),"Entreprise A", "Consultant It", DateOnly.Parse("2020/1/1"), DateOnly.Parse("2021/12/31"), "Développement d'applications web", "/images/p2.jpg"),
                new(new Guid(),"Entreprise B", "Consultant", DateOnly.Parse("2021/1/1"), DateOnly.Parse("2022/12/31"), "Création de jeux mobiles", "/images/p4.jpg")
            };

            var projects = new Project[]
            {
                new(new Guid(),"Portfolio Web", "Site personnel", "/images/p1.jpg", "https://github.com/..."),
                new(new Guid(),"Jeu Unity", "Mini-jeu mobile", "/images/p1.jpg", "/images/p2.jpg")
            };

            for (int i = 0; i < experiences.Length; i++)
                experiences[i].AddProject(projects[i]);
            
            foreach (var experience in experiences)
                admin.AddExperience(experience);

            context.AdminUsers.Add(admin);
            context.SaveChanges();
        }
    }
}