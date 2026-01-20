namespace PortfolioEAI.Domain.Entities.Ports
{
    public interface ISkillRepository
    {
        /// <summary>
        ///    Gets all skills from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Skill>> GetAll(CancellationToken ct);

        /// <summary>
        ///     Gets a skill by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Skill?> Get(Guid id, CancellationToken ct);
        
        /// <summary>
        ///   Gets skills by their name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Skill?> GetByName(string Name, CancellationToken ct);

        /// <summary>
        ///   Gets skills by their category.
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Skill>> GetAllByCategory(string Category, CancellationToken ct);
        
        /// <summary>
        ///     Adds a new skill to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Add(Skill entity, CancellationToken ct);

        /// <summary>
        ///    Updates an existing skill in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(Skill entity, CancellationToken ct);

        /// <summary>
        ///     Removes a skill from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Remove(Skill entity, CancellationToken ct);

        /// <summary>
        ///   Checks if a skill name already exists in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> SkillExists(Guid id, CancellationToken ct);
        
        /// <summary>
        ///   Checks if a skill name already exists in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ct"></param>
        /// <returns></returns>        
        Task<bool> SkillNameExists(string name, CancellationToken ct);
    }
}