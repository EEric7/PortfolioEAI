using PortfolioEAI.Application.DTOs;
using PortfolioEAI.Application.Mappings;
using PortfolioEAI.Application.Services.Interfaces;
using PortfolioEAI.Data.Repositories;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Application.Services
{
    public class ProjectService : IGenericServices<ProjectDto>
    {
        /// <summary>
        /// Service for managing projects.
        /// This service provides methods to retrieve, add, update, and delete projects.
        /// It uses a generic repository to interact with the data layer.
        /// </summary>
        private readonly IGenericRepository<Project> _projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="repository">The repository to use for project data operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repository is null.</exception>
        public ProjectService(IGenericRepository<Project> repository)
        {
            _projectRepository = repository;
        }

        /// <summary>
        /// Retrieves all projects.
        /// This method fetches all projects from the repository and maps them to DTOs.
        /// It returns an enumerable collection of <see cref="ProjectDto"/> objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => ProjectMapper.ToDto(p));
        }

        /// <summary>
        /// Retrieves a project by its identifier.
        /// This method fetches a project from the repository using its unique identifier.
        /// If the project is found, it is mapped to a <see cref="ProjectDto"/> and returned.
        /// If the project is not found, it returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var p = await _projectRepository.GetByIdAsync(id);

            if (p == null) return null;

            return ProjectMapper.ToDto(p);
        }

        /// <summary>
        /// Adds a new project.
        /// This method takes a <see cref="ProjectDto"/> as input, maps it to a <see cref="Project"/> entity,
        /// and adds it to the repository. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddAsync(ProjectDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Project cannot be null");
            }

            var project = ProjectMapper.ToEntity(dto);
            await _projectRepository.AddAsync(project);
        }
        
        /// <summary>
        /// Updates an existing project.
        /// This method takes a <see cref="ProjectDto"/> as input, retrieves the existing project by its identifier,
        /// and updates its properties based on the values in the DTO. It throws an <see cref="ArgumentNullException"/> if the DTO is null.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Deletes a project by its identifier.
        /// This method removes a project from the repository using its unique identifier.
        /// It does not return any value.
        /// If the project with the specified identifier does not exist, it does nothing.
        /// It is typically used to remove projects that are no longer needed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}