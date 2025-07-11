using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Models
{
    public class AccueilModel
    {
        [BindProperty]
        public AdminUserModel? AdminUserModel { get; set; } = default;

        [BindProperty]
        public List<SkillModel> SkillsModel { get; set; } = new List<SkillModel>();

        [BindProperty]
        public List<ProjectModel> ProjectsModel { get; set; } = new List<ProjectModel>();

        public AccueilModel(AdminUserDto dto)
        {
            AdminUserModel = new AdminUserModel(dto);
            foreach (var experience in AdminUserModel.Experiences)
            {
                if (experience.Projects != null && experience.Projects.Any())
                {
                    ProjectsModel.AddRange(experience.Projects.Select(projectDto => new ProjectModel(projectDto)));
                }
            }
            foreach (var skill in AdminUserModel.Skills)
            {
                var skillModel = new SkillModel(skill);
                SkillsModel.Add(skillModel);
            }
        }
        
    }
}