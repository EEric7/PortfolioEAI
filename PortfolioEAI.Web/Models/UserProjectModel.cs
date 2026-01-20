using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserModel : UserDto
    {

        //TODO: Move to resource on database.
        public string Profession { get; set; }
        public string EmailGitHub => "https://github.com/EEric7";
        public string PhoneNumber => "+33 6 12 34 56 78";

        public UserModel(UserDto dto) : base(dto)
        {
            Profession = dto.Roles.FirstOrDefault(x => x == "Profession") ?? "Développeur .NET";
        }
    }
}