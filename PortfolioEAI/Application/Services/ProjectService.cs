using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data.Repositories;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Services
{
    public class ProjectService : IGenericServices<ProjectDto>
    {
        private readonly IGenericRepository<Project> _projectRepository;

        public ProjectService(IGenericRepository<Project> repository)
        {
            _projectRepository = repository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => ProjectMapper.ToDto(p));
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var p = await _projectRepository.GetByIdAsync(id);

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
            await _projectRepository.AddAsync(project);
        }

        public async Task UpdateAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Project cannot be null");
            }

            var existing = await _projectRepository.GetByIdAsync(dto.Id);
            if (existing == null) return;
            
            if (dto.Title != null)
                existing.SetTitle(dto.Title);
            
            if (dto.ImageUrl != null)
                existing.SetImage(dto.ImageUrl);

            if (dto.Description != null)
                existing.SetDescription(dto.Description);

            if (dto.Url != null)
                existing.SetUrl(dto.Url);

            await _projectRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}