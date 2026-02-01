using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Web.Models;

namespace PortfolioEAI.Web.Application.Models
{
    public class AccueilModel : AMenuModel
    {
        [BindProperty]
        public UserModel? DTO { get; set; } = default;
        [BindProperty]
        public List<SkillModel> SkillsModelLanguages { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<SkillModel> SkillsModelFrameworks { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<SkillModel> SkillsModelDesign { get; set; } = new List<SkillModel>();
        [BindProperty]
        public List<ProjectModel> ProjectModelDisplay { get; set; } = new List<ProjectModel>();

        public AccueilModel() {}

        public void SetUserModel(UserDto userDto)
        {
            DTO = new UserModel(userDto);
            SetSkills(userDto.Skills);
        }

        private void SetSkills(IList<SkillDto> DTOs)
        {
            SkillsModelLanguages = DTOs
                .Where(s => s.Category == "Languages")
                .Select(s => new SkillModel(s))
                .OrderBy(z => z.Name)
                .ToList();

            SkillsModelFrameworks = DTOs
                .Where(s => s.Category == "Framework")
                .Select(s => new SkillModel(s))
                .OrderBy(z => z.Name)
                .ToList();

            SkillsModelDesign = DTOs
                .Where(s => s.Category == "Design")
                .Select(s => new SkillModel(s))
                .OrderBy(z => z.Name)
                .ToList();
        }
    }
}