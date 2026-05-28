using PortfolioEAI.Application.StoredFiles.DTOs;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Application.Mappings
{
    internal class StoredFileMapper
    {
        /// <summary>
        /// Maps a StoredFile entity to a StoredFileDto.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Maps a StoredFileDto to a StoredFile entity.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static StoredFile ToEntity(StoredFileDto? dto) 
        {
            return StoredFile.Create(
                dto?.Name ?? string.Empty,
                dto?.Type ?? string.Empty,
                dto?.Size ?? 0,
                dto?.Content ?? Array.Empty<byte>(),
                dto?.UploadedAt ?? DateTimeOffset.UtcNow
            );
        }
    }
}