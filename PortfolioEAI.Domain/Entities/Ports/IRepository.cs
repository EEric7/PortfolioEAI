using System.Linq.Expressions;

namespace PortfolioEAI.Domain.Entities.Ports
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///   Gets an entity by a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        /// <summary>
        ///  Gets all entities that match a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

        /// <summary>
        ///   Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Add(TEntity entity, CancellationToken ct);

        /// <summary>
        ///  Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(TEntity entity, CancellationToken ct);

        /// <summary>
        ///  Removes an entity from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Remove(TEntity entity, CancellationToken ct);

        /// <summary>
        ///   Checks if any entity exists that matches a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    }
}