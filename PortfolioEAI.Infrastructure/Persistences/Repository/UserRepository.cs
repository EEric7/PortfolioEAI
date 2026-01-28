using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities.Ports;
using System.Linq.Expressions;

namespace PortfolioEAI.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        /// <param name="logger">The logger to be used for logging operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context or logger is null.</exception>
        public UserRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context), Messages.NullError);

        /// <summary>
        ///  Adds a new user entity to the repository.
        /// </summary>
        /// <param name="entity">The user entity to add.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task Add(User entity, CancellationToken ct) => _context.Users.AddAsync(entity, ct).AsTask();

        /// <summary>
        ///     Checks if any user exists that matches a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> Exists(Expression<Func<User, bool>> predicate, CancellationToken ct) => await _context.Users.AnyAsync(predicate, ct);

        /// <summary>
        ///  Gets a user by a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User?> Get(Expression<Func<User, bool>> predicate, CancellationToken ct) => await _context.Users.FirstOrDefaultAsync(predicate, ct);

        /// <summary>
        ///     Gets all users that match a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllBy(Expression<Func<User, bool>> predicate, CancellationToken ct) => await _context.Users.Where(predicate).ToListAsync(ct);

        /// <summary>
        ///   Removes a user from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Remove(User entity, CancellationToken ct) => await Task.Run(() => _context.Users.Remove(entity), ct);

        /// <summary>
        ///  Updates an existing user in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Update(User entity, CancellationToken ct) => await Task.Run(() => _context.Users.Update(entity), ct);
    }
}