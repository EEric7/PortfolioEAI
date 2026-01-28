using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;

namespace PortfolioEAI.Infrastructure.Persistance.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.  
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// The logger instance for logging information and errors.
        /// This logger is used to log messages related to the operations performed by the ExperienceRepository.
        /// It helps in tracking the flow of execution and capturing any errors that may occur during database
        /// operations.
        /// </summary>
        public ExperienceRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");

        /// <summary>
        ///  Adds a new experience entity to the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Add(Experience entity, CancellationToken ct) => await _context.Experiences.AddAsync(entity, ct).AsTask();

        /// <summary>
        ///    Checks if any experience exists that matches a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> Exists(Expression<Func<Experience, bool>> predicate, CancellationToken ct) => await _context.Experiences.AnyAsync(predicate, ct);

        /// <summary>
        /// Gets an experience by a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Experience?> Get(Expression<Func<Experience, bool>> predicate, CancellationToken ct) => _context.Experiences.FirstOrDefaultAsync(predicate, ct);

        /// <summary>
        ///    Gets all experiences that match a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Experience>> GetAllBy(Expression<Func<Experience, bool>> predicate, CancellationToken ct) => await _context.Experiences.Where(predicate).ToListAsync(ct);

        /// <summary>
        ///  Removes an experience from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Remove(Experience entity, CancellationToken ct) => Task.Run(() => _context.Experiences.Remove(entity), ct);

        /// <summary>
        /// Updates an existing experience in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Update(Experience entity, CancellationToken ct) => Task.Run(() => _context.Experiences.Update(entity), ct);
    }
}