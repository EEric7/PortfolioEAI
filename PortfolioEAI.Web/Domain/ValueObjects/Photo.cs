using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEAI.Domain.ValueObjects
{
    public class Photo
    {
        /// <summary>
        /// Nom du fichier de la photo
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        /// URL de la photo
        /// </summary>
        public string Url { get; private set; } = string.Empty;

        /// <summary>
        /// Titre ou description de la photo
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// Date de création de la photo
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.MinValue;

        public Photo(string fileName, string url, string title, DateTime createdAt)
        {
            FileName = fileName;
            Url = url;
            Title = title;
            CreatedAt = createdAt;
        }
    }
}