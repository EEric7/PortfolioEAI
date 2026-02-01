using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance
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
                    context.Database.Migrate();

                    if (context.Users.Any())
                        context.Users.RemoveRange(context.Users);

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
                Skill.Create("C#", "Advanced", "Languages"),
                Skill.Create("SQL", "Intermediate", "Languages"),
                Skill.Create("HTML5, CSS3", "Intermediate", "Languages"),
                Skill.Create("DotNet Core Framwork", "Advanced", "Framwork"),
                Skill.Create("Entity Framwork", "Advanced", "Framwork"),
                Skill.Create("xUnit", "Intermediate", "Framwork"),
                Skill.Create("SQL Server", "Beginner", "Backend"),
                Skill.Create("Agiles développement & Scrum", "Advanced", "Design"),
                Skill.Create("Testing & Debugging", "Intermediate", "Design"),
                Skill.Create("SOLID Principales", "Advanced", "Design")
            };

            var projectsParagon = new Project[]
            {
                Project.Create("Logiciel de gestion d'impression de carte NFT.", "Pour répondre à un besoin d'externalisation d'un procédé d'impression et d'encodage de cartes, Paragon ID a développé un progiciel S-Printbox. Cette solution de gestion des impressions et d'encodage qui permet au client d'accéder aux services d'impression spécifique et d'utiliser les imprimantes dédiées.", "Consultant It", DateOnly.Parse("2018/6/10"), DateOnly.Parse("2021/9/25"))
            };

            var projectsEID = new Project[]
            {
                Project.Create("Application de gestion des interventions de maintenance.", "Dans le cadre d'une digitalisation des processus internes, Euro Information a mis en place une application web de gestion des interventions de maintenance. Cette application permet de planifier, suivre et documenter les interventions de maintenance sur les équipements informatiques de l'entreprise.","Apprentis manager en systèmes d’information", DateOnly.Parse("2022/3/1"), DateOnly.Parse("2024/12/31")),
                Project.Create("Système de reporting automatisé.", "Pour améliorer la prise de décision basée sur les données, Euro Information a développé un système de reporting automatisé. Ce système collecte des données à partir de diverses sources, les analyse et génère des rapports détaillés pour les équipes de gestion.", "Apprentis manager en systèmes d’information", DateOnly.Parse("2022/3/1"), DateOnly.Parse("2024/12/31")),
                Project.Create("Plateforme de formation en ligne pour les employés.", "Afin de favoriser le développement des compétences internes, Euro Information a lancé une plateforme de formation en ligne. Cette plateforme offre une variété de cours et de ressources éducatives pour aider les employés à améliorer leurs compétences techniques et professionnelles.", "Apprentis manager en systèmes d’information", DateOnly.Parse("2022/3/1"), DateOnly.Parse("2024/12/31"))
            };
            
            var expOne = Experience.Create("Euro Information", "Développement d'applications web");
            projectsEID.ToList().ForEach(p => expOne.AddProject(p));
            
            var expTwo = Experience.Create("Paragon ID", "Développement d'applications logiciel");
            projectsParagon.ToList().ForEach(p => expTwo.AddProject(p));

            var experiences = new Experience[]
            {
                expOne,
                expTwo
            };

            string description = @"Développeur .NET passionné et polyvalent avec plus de 5 ans d’expérience professionnelle et académique dans le développement de solutions innovantes.\r\n 
                                    Certifié Manager en systèmes d’information, développeur analyste développeur, j’ai construit ma carrière sur des bases solides en programmation, gestion de projets et migration vers le cloud.\r\n
                                    Je maîtrise des technologies telles que C#, SQL, HTML5/CSS3 et des frameworks comme ASP.NET Core,MVC et Blazor, en intégrant des bases de données complexes grâce à SQL Server et des outils comme Entity Framework.\r\n
                                    Mon expertise s’étend également aux méthodes Agiles, aux principes SOLID et aux design patterns.";
                                    
            var userAdmin =  User.Create("elembaadi@icloud.com","Admin321");
            userAdmin.SetRole("Admin");
            userAdmin.SetFirstname("ELEMBA ADI");
            userAdmin.SetLastname("Eric");
            userAdmin.SetDisplayName("ELEMBA ADi Eric");
            userAdmin.SetDescription(description);
            userAdmin.SetAddress("25 rue des carmes, 67100 Strasbourg, France");
            userAdmin.SetProfession("Développeur .NET");
            //userAdmin.SetPhoneNumber("+33 6 52 34 12 78");
            //userAdmin.SetURL("https://github.com/EEric7");

            skills.ToList().ForEach(s => userAdmin.AddSkill(s));
            experiences.ToList().ForEach(e => userAdmin.AddExperience(e));

            context.Users.Add(userAdmin);
            context.SaveChanges();
        }
    }
}