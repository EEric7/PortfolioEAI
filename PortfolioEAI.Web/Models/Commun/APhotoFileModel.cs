using Microsoft.AspNetCore.Mvc;
using PortfolioEAI.Application.StoredFiles.DTOs;

namespace PortfolioEAI.Web.Models
{
    public abstract class APhotoFileModel
    {
        [BindProperty]
        public IFormFile? PhotoFile { get; set; } = default;

        public async Task<StoredFileDto?> StoredFiles()
        {
           if (PhotoFile != null)
                {
                    // Process the uploaded photo file.
                    using var memoryStream = new MemoryStream();
                    await PhotoFile.CopyToAsync(memoryStream);

                    // Validate file size (example: max 10 MB)
                    if (memoryStream.Length > 10 * 1024 * 1024)
                        return null;

                    // For demonstration, we'll just set a placeholder value.
                    return new StoredFileDto 
                    {   
                        Name = PhotoFile.FileName,
                        Type = PhotoFile.ContentType,
                        Size = memoryStream.Length,
                        Content = memoryStream.ToArray(),
                        UploadedAt = DateTimeOffset.UtcNow
                    };
                }
                return null;
        }

        public async Task<IFormFile?> GetFiles(StoredFileDto? fileDto)
        {
            if (fileDto != null)
            {
                // Create a new IFormFile from the StoredFileDto.
                if (fileDto.Content == null || fileDto.Content.Length == 0)
                    return null;

                var stream = new MemoryStream(fileDto.Content);

                return new FormFile(stream, 0, fileDto.Size ?? 0, fileDto.Name ?? string.Empty, fileDto.Name ?? string.Empty)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = fileDto.Type ?? "application/octet-stream"
                };
            }
            return null;
        }
    }
}