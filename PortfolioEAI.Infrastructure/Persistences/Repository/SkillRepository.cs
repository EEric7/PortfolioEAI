using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;
using PortfolioEAI.Domain.Enums;
using PortfolioEAI.Domain.Ressources;

namespace PortfolioEAI.Infrastructure.Persistance.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        /// <summary>
        /// Represents the database context for accessing data.
        /// This context is used to interact with the database, allowing for operations such as
        /// querying, adding, updating, and deleting entities.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillRepository"/> class with the specified database context.
        /// This constructor is typically used for dependency injection in ASP.NET Core applications.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public SkillRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context), Messages.NullError);

        /// <summary>
        /// Gets all skills from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Skill>> GetAll(CancellationToken ct) => await _context.Skills.AsNoTracking().ToListAsync(ct);

        /// <summary>
        /// Gets all skills from the database.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Skill?> Get(Guid id, CancellationToken ct) => await _context.Skills.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);

        /// <summary>
        ///    Gets skills by their name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Skill?> GetByName(string Name, CancellationToken ct) => await _context.Skills.AsNoTracking().Where(s => s.Name == Name).FirstOrDefaultAsync(ct);

        /// <summary>
        ///     Gets skills by their category.
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Skill>> GetAllByCategory(string Category, CancellationToken ct) => await _context.Skills.AsNoTracking().Where(s => s.Category == Enum.Parse<SkillCategory>(Category)).ToListAsync(ct);

        /// <summary>
        /// Adds a new skill to the database.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ct"></param>
        public async Task Add(Skill entity, CancellationToken ct) => await _context.Skills.AddAsync(entity, ct);

        /// <summary>
        ///     Updates an existing skill in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Update(Skill entity, CancellationToken ct)
        {
            _context.Skills.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Removes a user from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task Remove(Skill entity, CancellationToken ct)
        {
            _context.Skills.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        ///  Checks if a skill exists by their unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> SkillExists(Guid id, CancellationToken ct) => await _context.Skills.AnyAsync(s => s.Id == id, ct);

        /// <summary>
        ///     Checks if a skill name already exists in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> SkillNameExists(string name, CancellationToken ct) => await _context.Skills.AnyAsync(s => s.Name == name, ct);
    }
}