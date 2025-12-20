using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Models
{
    public class AdminUserModel : AdminUserDto
    {
        public string Street => "25 rue des carmes";
        public string City => "Strasbourg";
        public string PostalCode => "67100";
        public string Country => "France";
        public string Number => "06 16 55 13 36";
        public string Profession => "Développeur .NET";
        public string EmailGitHub => "https://github.com/EEric7";
        
        [BindProperty]
        public List<SkillModel> SkillsModel { get; set; } = new List<SkillModel>();

        [BindProperty]
        public List<ExperienceModel> ExperienceModel { get; set; } = new List<ExperienceModel>();

        public AdminUserModel(AdminUserDto dto) : base(dto)
        {
            SkillsModel = dto.Skills.Select(skillDto => new SkillModel(skillDto)).ToList();
            ExperienceModel = dto.Experiences.Select(experienceDto => new ExperienceModel(experienceDto)).ToList();
        }

        public string GetFullAddress()
        {
            return $"{Street}, {PostalCode} {City}, {Country}";
        }
    }
}