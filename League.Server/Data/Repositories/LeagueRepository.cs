using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Data;
using League.Server.Data.Repositories.Interfaces;
using League.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Server.Data.Repositories
{
    /// <summary>
    /// Repository for managing league data in the database
    /// </summary>
    public class LeagueRepository : GenericRepository<Models.League>, ILeagueRepository
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<Models.League> _leagues;
        private readonly ILogger<LeagueRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public LeagueRepository(LeagueDbContext context, ILogger<LeagueRepository> logger)
            : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _leagues = context.Set<Models.League>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets leagues containing the specified name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of leagues matching the search criteria</returns>
        public async Task<IEnumerable<Models.League>> GetLeaguesByNameAsync(
            string name,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching leagues by name: {Name}", name);
            return await _leagues
                .AsNoTracking()
                .Where(league => league.Name!.Contains(name))
                .ToListAsync(cancellationToken);
        }
    }
}
