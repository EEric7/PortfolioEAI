using PortfolioEAI.Web.Domain.Entities; // Ensure this is the correct namespace for 'Project'

namespace PortfolioEAI.Web.Data.Repositorys.Interfaces
{
    public interface IProjectRepository
    {
       /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all dataset.</returns>
        Task<IEnumerable<Project>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        Task<Project?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(Project entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        Task UpdateAsync(Project entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        Task DeleteAsync(Guid id);
    }
}