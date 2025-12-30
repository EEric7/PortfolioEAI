using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Web.Application.DTOs;
using Serilog.Debugging;

namespace PortfolioEAI.Web.Application.Models
{
    public class AdminUserModel : AdminUserDto
    {

        //TODO: Move to resource on database.
        public string Profession => "Développeur .NET";
        public string EmailGitHub => "https://github.com/EEric7";
        public string PhoneNumber => "+33 6 12 34 56 78";

        public AdminUserModel(AdminUserDto dto) : base(dto)
        {
            
        }
    }
}