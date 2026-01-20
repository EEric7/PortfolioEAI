using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Mappings
{
    internal class UserMapper
    {
        /// <summary>
        /// Maps a User domain entity to a UserDto.
        /// </summary>
        /// <param name="user">The User entity to map.</param>
        /// <returns>A UserDto representing the mapped User entity.</returns>
        public static UserDto ToDto(User user) => new UserDto
        {
            Id = user.Id,
            UserName = user.DisplayName,
            Email = user.Email.Value,
            Description = user.Description,
            Address = user.Address?.GetFullAddress(),
            Skills = user.Skills.Select(s => s.Id).ToList(),
            Experiences = user.Experiences.Select(e => e.Id).ToList()
        };
    }
}