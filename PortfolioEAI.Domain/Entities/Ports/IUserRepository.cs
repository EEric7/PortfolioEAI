namespace PortfolioEAI.Domain.Entities.Ports
{
    public interface IUserRepository
    {
        /// <summary>
        ///    Gets all users from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAll(CancellationToken ct);

        /// <summary>
        ///     Gets a user by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<User?> Get(Guid id, CancellationToken ct);

        /// <summary>
        ///     Gets a user by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User?> GetByEmail(string email, CancellationToken ct);

        /// <summary>
        ///     Adds a new user to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Add(User entity, CancellationToken ct);

        /// <summary>
        ///     Updates an existing user in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Update(User entity, CancellationToken ct);

        /// <summary>
        ///    Removes a user from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task Remove(User entity, CancellationToken ct);

        /// <summary>
        ///   Checks if an email already exists in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> EmailExists(string email, CancellationToken ct);

        /// <summary>
        ///   Checks if a user exists by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> UserExists(Guid id, CancellationToken ct);
    }
}