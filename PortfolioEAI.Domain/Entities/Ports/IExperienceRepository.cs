namespace PortfolioEAI.Domain.Entities.Ports
{
    public interface IExperienceRepository
    {
        /// <summary>
        ///     Gets all experiences from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Experience>> GetAll(CancellationToken ct);

        /// <summary>
        ///    Gets an experience by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Experience?> Get(Guid id, CancellationToken ct);

        /// <summary>
        ///   Gets an experience by company name.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Experience?> GetByCompany(string company, CancellationToken ct);

        /// <summary>
        ///    Adds a new experience to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Add(Experience entity, CancellationToken ct);

        /// <summary>
        ///    Updates an existing experience in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(Experience entity, CancellationToken ct);

        /// <summary>
        ///   Removes an experience from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Remove(Experience entity, CancellationToken ct);

        /// <summary>
        ///   Checks if an experience exists in the database by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> Exists(Guid id, CancellationToken ct);

        /// <summary>
        ///   Checks if an experience exists in the database by company name.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> ExistsByCompany(string company, CancellationToken ct);
    }
}