using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEAI.Web.Application.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(IFormFile file, string folderPath);
        Task<bool> DeletePhotoAsync(string filePath);
        Task<List<string>> UploadPhotosAsync(IFormFileCollection files, string folderPath);
        bool IsValidPhotoFile(IFormFile file);
        string GetPhotoUrl(string fileName);
    }
}