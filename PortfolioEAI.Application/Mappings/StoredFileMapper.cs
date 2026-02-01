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
    }
}