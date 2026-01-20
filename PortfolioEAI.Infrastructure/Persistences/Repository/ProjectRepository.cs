using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Infrastructure.Persistance.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context), Messages.NullError);

        /// <summary>
        /// Gets all projects from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Project>> GetAll(CancellationToken ct) => await _context.Projects.AsNoTracking().ToListAsync(ct);

        /// <summary>
        /// Gets all projects by URL from the database.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Project>> GetAllByUrl(string url, CancellationToken ct) => await _context.Projects
        .AsNoTracking()
        .Where(p => p.Url != null && p.Url.Value.Contains(url))
        .ToListAsync(ct);
        
        /// <summary>
        /// Gets a project by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Project?> Get(Guid id, CancellationToken ct) => _context.Projects.FirstOrDefaultAsync(o => o.Id == id, ct);

        /// <summary>
        /// Gets a project by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Project?> GetByTitle(string title, CancellationToken ct) => _context.Projects.FirstOrDefaultAsync(o => o.Title == title, ct);

        /// <summary>
        /// Adds a new project to the database.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Add(Project p, CancellationToken ct) => _context.Projects.AddAsync(p, ct).AsTask();

        /// <summary>
        ///    Updates an existing project in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Update(Project entity, CancellationToken ct)
        {
            _context.Projects.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///   Removes a project from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Remove(Project entity, CancellationToken ct)
        {
            _context.Projects.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///   Checks if a project exists in the database by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> Exists(Guid id, CancellationToken ct) => await _context.Projects.AsNoTracking().AnyAsync(e => e.Id == id, ct);

        /// <summary>
        ///  Checks if a project exists in the database by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByTitle(string title, CancellationToken ct) => await _context.Projects.AsNoTracking().AnyAsync(e => e.Title == title, ct);
    }
}