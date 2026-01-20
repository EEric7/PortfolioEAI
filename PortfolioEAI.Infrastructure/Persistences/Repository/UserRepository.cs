using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Ressources;
using PortfolioEAI.Domain.Entities.Ports;

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
        /// Gets all users from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll(CancellationToken ct) => await _context.Users.AsNoTracking().ToListAsync(ct);
        
        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User?> Get(Guid id, CancellationToken ct) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, ct);

        /// <summary>
        ///    Gets a user by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User?> GetByEmail(string email, CancellationToken ct) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Value == email, ct);

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Add(User entity, CancellationToken ct) => await _context.Users.AddAsync(entity, ct);

        /// <summary>
        ///     Updates an existing user in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Update(User entity, CancellationToken ct)
        {
            _context.Users.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Removes a user from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Remove(User entity, CancellationToken ct)
        {
            _context.Users.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///  Checks if an email already exists in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> EmailExists(string email, CancellationToken ct) => await _context.Users.AnyAsync(u => u.Email.Value == email, ct);

        /// <summary>
        ///  Checks if a user exists by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> UserExists(Guid id, CancellationToken ct) => await _context.Users.AnyAsync(u => u.Id == id, ct);
    }
}