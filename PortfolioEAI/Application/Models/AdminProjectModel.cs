using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Models
{
    public class AdminUserModel : AdminUserDto
    {
        public string FullName => $"ELEMBA ADI Eric";
        public string Number => "06 16 55 13 36";
        public string EmailGitHub => "https://github.com/EEric7";

        public AdminUserModel(AdminUserDto dto) : base(dto)
        {
            // Additional initialization or properties can be added here if needed
        }
    }
}