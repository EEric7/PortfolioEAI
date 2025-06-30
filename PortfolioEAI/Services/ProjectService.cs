using PortfolioEAI.Models;
using PortfolioEAI.Repositories;

namespace PortfolioEAI.Services
{
    public class ProjectService : IGenericServices<Project>
    {
        private readonly IRepository _repository;

        public ProjectService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _repository.Projects.GetAllAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _repository.Projects.GetByIdAsync(id);
        }

        public async Task AddAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project), "Project cannot be null");
            }
            await _repository.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project), "Project cannot be null");
            }
            await _repository.Projects.UpdateAsync(project);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid project ID", nameof(id));
            }
            await _repository.Projects.DeleteAsync(id);
        }
    }
}