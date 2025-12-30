using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Models
{
    public class AccueilModel 
    {
        [BindProperty]
        public AdminUserModel? AdminUserModel { get; set; } = default;
        [BindProperty]
        public List<SkillModel> SkillsModelLanguages { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<SkillModel> SkillsModelFrameworks { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<SkillModel> SkillsModelDesign { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<ProjectModel> ProjectModelDisplay { get; set; } = new List<ProjectModel>();

        public AccueilModel() {}

        public AccueilModel(AdminUserDto dto)
        {
            AdminUserModel = new AdminUserModel(dto);

            SkillsModelLanguages = AdminUserModel.Skills.Select(x => new SkillModel(x))
                                    .Where(s => s.Category == Domain.Enums.SkillCategory.Languages)
                                    .OrderBy(z => z.Name)
                                    .ToList();

            SkillsModelFrameworks = AdminUserModel.Skills.Select(x => new SkillModel(x))
                                    .Where(s => s.Category == Domain.Enums.SkillCategory.Framwork)
                                    .OrderBy(z => z.Name)
                                    .ToList();

            SkillsModelDesign = AdminUserModel.Skills.Select(x => new SkillModel(x))
                                .Where(s => s.Category == Domain.Enums.SkillCategory.Design)
                                .OrderBy(z => z.Name)
                                .ToList();

            ProjectModelDisplay = AdminUserModel.Experiences.SelectMany(e => e.Projects)
                                    .Select(x => new ProjectModel(x))
                                    .Take(3)
                                    .ToList() ?? new List<ProjectModel>();
        }
        
    }
}