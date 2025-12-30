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

                    if (context.AdminUsers.Any())
                        context.AdminUsers.RemoveRange(context.AdminUsers);

                    if(context.Skills.Any())
                        context.Skills.RemoveRange(context.Skills);

                    if (context.Experiences.Any())
                        context.Experiences.RemoveRange(context.Experiences);
                        
                    if (context.Projects.Any())
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
            var skills = new Skill[]
            {
                new(new Guid(),"C#", SkillLevel.Advanced, SkillCategory.Languages),
                new(new Guid(),"SQL", SkillLevel.Intermediate, SkillCategory.Languages),
                new(new Guid(),"HTML5, CSS3", SkillLevel.Intermediate, SkillCategory.Languages),
                new(new Guid(),"DotNet Core Framwork", SkillLevel.Advanced, SkillCategory.Framwork),
                new(new Guid(),"Entity Framwork", SkillLevel.Advanced, SkillCategory.Framwork),
                new(new Guid(),"xUnit", SkillLevel.Intermediate, SkillCategory.Framwork),
                new(new Guid(),"SQL Server", SkillLevel.Beginner, SkillCategory.Backend),
                new(new Guid(),"Agiles développement & Scrum", SkillLevel.Advanced, SkillCategory.Design),
                new(new Guid(),"Testing & Debugging", SkillLevel.Intermediate, SkillCategory.Design),
                new(new Guid(),"SOLID Principales", SkillLevel.Advanced, SkillCategory.Design)
            };

            var projectsParagon = new Project[]
            {
                new(new Guid(),"Logiciel de gestion d'impression de carte NFT.", "Pour répondre à un besoin d'externalisation d'un procédé d'impression et d'encodage de cartes, Paragon ID a développé un progiciel S-Printbox. Cette solution de gestion des impressions et d'encodage qui permet au client d'accéder aux services d'impression spécifique et d'utiliser les imprimantes dédiées.", "assets/img/PID.jpg", "https://github.com/...")
            };

            var projectsEID = new Project[]
            {
                new(new Guid(),"Service web de déploiement des solution cognitive", "Dans le cadre d'une migration d'une solution de déploiement cognitif, le secteur h230 migre la solution webfarm vers un environement cloud propriétaire et réalise une refonte complète du projet afin de simplifier l'accès et les services deployment des outils cognitifs du groupe.", "assets/img/Wacken.jpg", "https://github.com/..."),
                new(new Guid(),"Service web de signature électronique", "Dans le cadre d'un projet d'optimisassions du web service DSIG, qui offre des solutions de signature électronique évoluer via aux partis prenant certifier, une refonte des interfaces graphique, l'amélioration de plusieurs services et l'ajout d'un nouveau organisme de signature.", "assets/img/Wacken2.jpg", "https://github.com/..."),
            };

            var experiences = new Experience[]
            {
                new(new Guid(),"Euro Information", "Consultant It", DateOnly.Parse("2022/3/1"), DateOnly.Parse("2024/12/31"), "Développement d'applications web", "/images/p2.jpg", projectsEID),
                new(new Guid(),"Paragon ID", "Apprentis manager en systèmes d’information", DateOnly.Parse("2018/9/1"), DateOnly.Parse("2021/7/25"), "Développement d'applications logiciel", "/images/p4.jpg", projectsParagon)
            };

            string description = @"Développeur .NET passionné et polyvalent avec plus de 5 ans d’expérience professionnelle et académique dans le développement de solutions innovantes.\r\n 
                                    Certifié Manager en systèmes d’information, développeur analyste développeur, j’ai construit ma carrière sur des bases solides en programmation, gestion de projets et migration vers le cloud.\r\n
                                    Je maîtrise des technologies telles que C#, SQL, HTML5/CSS3 et des frameworks comme ASP.NET Core,MVC et Blazor, en intégrant des bases de données complexes grâce à SQL Server et des outils comme Entity Framework.\r\n
                                    Mon expertise s’étend également aux méthodes Agiles, aux principes SOLID et aux design patterns.";
                                    
            var admin = new AdminUser(new Guid(), "ELEMBA ADI Eric", "admin321", "elembaadi@icloud.com", description, "25 rue des carmes, 67100 Strasbourg, France", skills, experiences);

            context.AdminUsers.Add(admin);
            context.SaveChanges();
        }
    }
}