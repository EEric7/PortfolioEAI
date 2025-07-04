using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Repositorys.Interfaces
{
    public interface IExperienceRepository
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all dataset.</returns>
        Task<IEnumerable<Experience>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        Task<Experience?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(Experience entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        Task UpdateAsync(Experience entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        Task DeleteAsync(Guid id);
    }
}