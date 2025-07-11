using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Models
{
    public class AdminUserModel : AdminUserDto
    {
        public AdminUserModel(AdminUserDto dto) : base(dto)
        {
            // Additional initialization or properties can be added here if needed
        }
    }
}