using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

namespace PortfolioEAI.Application.Mappings
{
    public class AdminUserMapper
    {
        public static AdminUser ToEntity(AdminUserDto dto)
        {
            var AdminUser = new AdminUser(
                dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                dto.UserName ?? string.Empty,
                dto.Password ?? string.Empty,
                dto.Email ?? string.Empty);
            return AdminUser;
        }   
        
       
        public static AdminUserDto ToDto(AdminUser user) => new AdminUserDto
        {
            Id = user.Id,
            UserName = user.Username,
            Email = user.Email.Value,
            Password = user.Password,
            Skills = user.Skills.Select(s => s.Id).ToList()
        };
    }
}