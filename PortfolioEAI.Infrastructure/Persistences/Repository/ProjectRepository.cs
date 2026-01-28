using System.Linq.Expressions;
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
        /// Adds a new project to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Add(Project entity, CancellationToken ct) => await _context.Projects.AddAsync(entity, ct);

        /// <summary>
        ///   Checks if any project exists that matches a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> Exists(Expression<Func<Project, bool>> predicate, CancellationToken ct) => await _context.Projects.AnyAsync(predicate, ct);

        /// <summary>
        /// Gets a project by a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Project?> Get(Expression<Func<Project, bool>> predicate, CancellationToken ct) => await _context.Projects.FirstOrDefaultAsync(predicate, ct);

        /// <summary>
        ///   Gets all projects that match a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Project>> GetAllBy(Expression<Func<Project, bool>> predicate, CancellationToken ct) => await  _context.Projects.Where(predicate).ToListAsync(ct);

        /// <summary>
        ///     Removes a project from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Remove(Project entity, CancellationToken ct) => await Task.Run(() => _context.Projects.Remove(entity), ct);

        /// <summary>
        ///   Updates an existing project in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Update(Project entity, CancellationToken ct) => await Task.Run(() => _context.Projects.Update(entity), ct);
    }
}