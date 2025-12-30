using PortfolioEAI.Web.Domain.Entities;

namespace PortfolioEAI.Web.Domain.Services.Interfaces
{
    public interface IAdminUserService
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all dataset.</returns>
        /// <remarks>This method retrieves all entities of type T from the data source.</remarks>
        /// <example>
        /// <code>
        /// var allEntities = service.GetAllAsync();
        /// </code>
        /// </example>  
        Task <IEnumerable<AdminUser>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method retrieves an entity of type T by its unique identifier.
        /// </remarks>
        /// <example>
        /// <code>
        /// var entity = await service.GetByIdAsync(1);
        /// </code>
        /// </example>
        Task <AdminUser?> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <remarks>
        /// This method adds a new entity of type T to the data source.
        /// </remarks>
        /// <example>
        /// <code>
        /// await service.AddAsync(newEntity);
        /// </code>
        /// </example>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// returns>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the entity cannot be added.</exception
        Task AddAsync(AdminUser entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <remarks>
        /// This method updates an existing entity of type T in the data source.
        /// </remarks>
        /// <example>
        /// <code>
        /// await service.UpdateAsync(existingEntity);
        Task UpdateAsync(AdminUser entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}