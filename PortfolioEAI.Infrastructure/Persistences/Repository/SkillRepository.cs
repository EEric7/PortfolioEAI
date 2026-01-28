using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Entities.Ports;
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
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SkillRepository(ApplicationDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context), Messages.NullError);

        /// <summary>
        /// Adds a new skill to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Add(Skill entity, CancellationToken ct) => await _context.Skills.AddAsync(entity, ct);

        /// <summary>
        ///   Checks if any skill exists that matches a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Exists(Expression<Func<Skill, bool>> predicate, CancellationToken ct) =>  await _context.Skills.AnyAsync(predicate, ct);

        /// <summary>
        /// Gets a skill by a specified predicate.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Skill?> Get(Expression<Func<Skill, bool>> predicate, CancellationToken ct) => await _context.Skills.FirstOrDefaultAsync(predicate, ct);

        /// <summary>
        /// Gets all skills that match a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Skill>> GetAllBy(Expression<Func<Skill, bool>> predicate, CancellationToken ct) => await _context.Skills.Where(predicate).ToListAsync(ct);

        /// <summary>
        /// Removes a skill from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Remove(Skill entity, CancellationToken ct) => await Task.Run(() => _context.Skills.Remove(entity), ct);

        /// <summary>
        ///    Updates an existing skill in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task Update(Skill entity, CancellationToken ct) => await Task.Run(() => _context.Skills.Update(entity), ct);
    }
}