using PortfolioEAI.Application.StoredFiles.DTOs;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Application.Mappings
{
    internal class StoredFileMapper
    {
        public static StoredFileDto ToDto(StoredFile entity) 
        {
            return new StoredFileDto{
            Name = entity.Name ?? string.Empty,
            Type = entity.Type ?? string.Empty,
            Size = entity.Size,
            Content = entity.Content,
            UploadedAt = entity.UploadedAt
            };
        }

        public static StoredFile ToEntity(StoredFileDto? dto) 
        {
            return StoredFile.Create(
                dto!.Name,
                dto!.Type,
                dto!.Size,
                dto!.Content,
                dto!.UploadedAt
            );
        }
    }
}