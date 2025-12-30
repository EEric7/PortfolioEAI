using PortfolioEAI.Web.Application.DTOs;
using PortfolioEAI.Web.Domain.Entities;

namespace PortfolioEAI.Web.Application.Mappings
{
    internal class AdminUserMapper
    {
        public static AdminUser ToEntity(AdminUserDto dto)
        {
            var AdminUser = new AdminUser(
                dto.Id ?? Guid.NewGuid(),
                dto.UserName ?? string.Empty,
                dto.Password ?? string.Empty,
                dto.Email ?? string.Empty,
                dto.Description ?? string.Empty,
                dto.Address ?? string.Empty,
                (dto.Skills ?? new List<SkillDto>()).Select(s => SkillMapper.ToEntity(s)).ToList(),
                (dto.Experiences ?? new List<ExperienceDto>()).Select(e => ExperienceMapper.ToEntity(e)).ToList()
            );
            return AdminUser;
        }   
        
       
        public static AdminUserDto ToDto(AdminUser user) => new AdminUserDto
        {
            Id = user.Id,
            UserName = user.Username,
            Email = user.Email.Value,
            Password = user.Password,
            Description = user.Description,
            Address = user.Address?.GetFullAddress(),
            Skills = user.Skills.Select(s => SkillMapper.ToDto(s)).ToList(),
            Experiences = user.Experiences.Select(e => ExperienceMapper.ToDto(e)).ToList()
        };
    }
}