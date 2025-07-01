using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Domain.Interfaces;

namespace PortfolioEAI.Application.Services
{
    public class ProjectService : IGenericServices<ProjectDto>
    {
        private readonly IRepository _repository;

        public ProjectService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.Projects.GetAllAsync();
            return projects.Select(p => ProjectMapper.ToDto(p));
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var p = await _repository.Projects.GetByIdAsync(id);

            if (p == null) return null;

            return ProjectMapper.ToDto(p);
        }

        public async Task AddAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Project cannot be null");
            }

            var project = ProjectMapper.ToEntity(dto);
            await _repository.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Project cannot be null");
            }

            var existing = await _repository.Projects.GetByIdAsync(dto.Id);
            if (existing == null) return;
            
            if (dto.Title != null)
                existing.SetTitle(dto.Title);
            
            if (dto.ImageUrl != null)
                existing.SetImage(dto.ImageUrl);

            if (dto.Description != null)
                existing.SetDescription(dto.Description);

            if (dto.Url != null)
                existing.SetUrl(dto.Url);

            await _repository.Projects.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.Projects.DeleteAsync(id);
        }
    }
}