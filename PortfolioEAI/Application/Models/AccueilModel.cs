using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Application.Models
{
    public class AccueilModel 
    {
        [BindProperty]
        public AdminUserModel? AdminUserModel { get; set; } = default;

        public AccueilModel(AdminUserDto dto)
        {
            AdminUserModel = new AdminUserModel(dto);
        }
        
    }
}