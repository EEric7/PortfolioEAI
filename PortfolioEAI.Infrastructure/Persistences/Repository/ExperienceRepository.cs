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
        /// Gets all experiences from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Experience>> GetAll(CancellationToken ct) => await _context.Experiences.AsNoTracking().ToListAsync(ct);
        
        /// <summary>
        /// Gets a project by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Experience?> Get(Guid id, CancellationToken ct) => await _context.Experiences.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, ct);

        /// <summary>
        /// Gets an experience by company name.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Experience?> GetByCompany(string company, CancellationToken ct) => await _context.Experiences.AsNoTracking().FirstOrDefaultAsync(o => o.Company == company, ct);

        /// <summary>
        /// Adds a new experience to the database.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Add(Experience p, CancellationToken ct) => _context.Experiences.AddAsync(p, ct).AsTask();

        /// <summary>
        ///     Updates an existing experience in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Update(Experience entity, CancellationToken ct)
        {
            _context.Experiences.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Removes an experience from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Remove(Experience entity, CancellationToken ct)
        {
            _context.Experiences.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///    Checks if an experience exists in the database by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> Exists(Guid id, CancellationToken ct) => _context.Experiences.AsNoTracking().AnyAsync(e => e.Id == id, ct);

        /// <summary>
        ///   Checks if an experience exists in the database by company and position.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByCompany(string company, CancellationToken ct) => await _context.Experiences.AsNoTracking().AnyAsync(e => e.Company == company, ct);
    }
}