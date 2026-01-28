using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.StoredFiles.DTOs;

namespace PortfolioEAI.Web.Models
{
    public class UserCreateModel : AMenuDashbordModel
    {
        public UserCreateModel() {}

        [BindProperty]
        public UserDto DTO { get; set; } = default!;

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

        [BindProperty]
        public List<SelectListItem> RoleOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Admin" },
            new SelectListItem { Value = "2", Text = "User" },
            new SelectListItem { Value = "3", Text = "Visitor" }
        };

        public async Task<bool> StoredFiles()
        {
           if (PhotoFile != null)
                {
                    // Process the uploaded photo file.
                    using var memoryStream = new MemoryStream();
                    await PhotoFile.CopyToAsync(memoryStream);

                    // Validate file size (example: max 10 MB)
                    if (memoryStream.Length > 10 * 1024 * 1024)
                        return false;

                    // For demonstration, we'll just set a placeholder value.
                    DTO.ProfilePhoto = new StoredFileDto 
                    { 
                        Name = PhotoFile.FileName,
                        Type = PhotoFile.ContentType,
                        Size = memoryStream.Length,
                        Content = memoryStream.ToArray(),
                        UploadedAt = DateTimeOffset.UtcNow
                    };
                }
                return true;
        }
    }
}