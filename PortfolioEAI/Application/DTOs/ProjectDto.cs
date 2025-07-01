namespace PortfolioEAI.Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; private set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
    }
}