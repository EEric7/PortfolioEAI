namespace PortfolioEAI.Application.StoredFiles.DTOs
{
    public class StoredFileDto
    {
        /// <summary>
        ///  Name of the photo
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Type of the photo
        /// </summary>
        public string? Type { get; set; }
        
        /// <summary>
        /// Size of the photo
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// Content of the photo
        /// </summary>
        public byte[]? Content { get; set; }

        /// <summary>
        /// Date and time when the photo was uploaded
        /// </summary>
        public DateTimeOffset? UploadedAt { get; set; }

        /// <summary>
        /// Data URL ready for use in an img src attribute.
        /// </summary>
        public string? ImageDataUrl
        {
            get
            {
                if (Content == null || Content.Length == 0)
                {
                    return null;
                }

                var mimeType = string.IsNullOrWhiteSpace(Type) ? "image/jpeg" : Type;
                return $"data:{mimeType};base64,{Convert.ToBase64String(Content)}";
            }
        }
    }
}