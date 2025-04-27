using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Data.Repositories.Interfaces;
using League.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace League.Server.Data.Repositories
{
    /// <summary>
    /// Repository for managing conference data in the database
    /// </summary>
    public class ConferenceRepository : GenericRepository<Conference>, IConferenceRepository
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<Conference> _conferences;
        private readonly ILogger<ConferenceRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConferenceRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public ConferenceRepository(LeagueDbContext context, ILogger<ConferenceRepository> logger)
            : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _conferences = context.Set<Conference>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets conferences belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of conferences in the specified league</returns>
        public async Task<IEnumerable<Conference>> GetConferencesByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching conferences by League ID: {LeagueId}", leagueId);

            return await _conferences
                .AsNoTracking()
                .Where(c => c.LeagueId == leagueId)
                .ToListAsync(cancellationToken);
        }
    }
}
