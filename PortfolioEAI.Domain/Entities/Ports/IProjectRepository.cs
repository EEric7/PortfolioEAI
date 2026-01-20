namespace PortfolioEAI.Domain.Entities.Ports
{
    public interface IProjectRepository
    {
        /// <summary>
        ///   Gets all projects from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Project>> GetAll(CancellationToken ct);

        /// <summary>
        /// Gets all projects by URL from the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Project>> GetAllByUrl(string url, CancellationToken ct);

        /// <summary>
        ///    Gets a project by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Project?> Get(Guid id, CancellationToken ct);

        /// <summary>
        ///   Gets a project by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Project?> GetByTitle(string title, CancellationToken ct);
        
        /// <summary>
        ///     Adds a new project to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Add(Project entity, CancellationToken ct);

        /// <summary>
        ///    Updates an existing project in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(Project entity, CancellationToken ct);

        /// <summary>
        ///  Removes a project from the database.   
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Remove(Project entity, CancellationToken ct);

        /// <summary>
        ///   Checks if a project exists in the database by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> Exists(Guid id, CancellationToken ct);

        /// <summary>
        ///  Checks if a project exists in the database by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> ExistsByTitle(string title, CancellationToken ct);
    }
}