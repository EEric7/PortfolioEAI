using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioEAI.Application.Services.Interfaces;

namespace PortfolioEAI.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<PhotoService> _logger;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public PhotoService(IWebHostEnvironment environment, ILogger<PhotoService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        /// <summary>
        /// Supprime une photo donnée son chemin de fichier.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Task<bool> DeletePhotoAsync(string filePath)
        {
            try
            {
                string fullPath = Path.Combine(_environment.WebRootPath, filePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation("Photo deleted successfully: {FilePath}", filePath);
                    return Task.FromResult(true);
                }

                _logger.LogWarning("Photo file not found: {FilePath}", filePath);
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting photo: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        /// <summary>
        ///  Retourne l'URL complète de la photo à partir du nom de fichier.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetPhotoUrl(string fileName)
        {
            return $"/uploads/{fileName}";
        }

        /// <summary>
        /// Valide si le fichier est une photo autorisée.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsValidPhotoFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxFileSize)
            {
                _logger.LogWarning("File too large: {FileName} ({FileSize} bytes)", file.FileName, file.Length);
                return false;
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                _logger.LogWarning("File type not allowed: {Extension}", extension);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Upload une photo et retourne son URL.
        /// </summary>
        public async Task<string> UploadPhotoAsync(IFormFile file, string folderPath)
        {
            try
            {
                if (!IsValidPhotoFile(file))
                {
                    throw new InvalidOperationException("Fichier invalide ou non autorisé.");
                }

                string uploadsFolder = Path.Combine(_environment.WebRootPath, folderPath);
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _logger.LogInformation("Photo uploaded successfully: {FileName}", uniqueFileName);
                return $"/{folderPath}/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading photo: {ErrorMessage}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Upload plusieurs photos et retourne leurs URLs
        /// </summary>
        /// <param name="files"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public async Task<List<string>> UploadPhotosAsync(IFormFileCollection files, string folderPath)
        {
             var urls = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    var url = await UploadPhotoAsync(file, folderPath);
                    urls.Add(url);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading photo batch: {ErrorMessage}", ex.Message);
                }
            }

            return urls;
        }
    }
}