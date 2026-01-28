using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserModel : UserDto
    {
        public string EmailGitHub => "https://github.com/EEric7";
        public string PhoneNumber => "+33 6 12 34 56 78";

        public UserModel(UserDto dto) : base(dto) {}
    }
}