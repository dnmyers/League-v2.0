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
    /// Repository for managing division data in the database
    /// </summary>
    public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<Division> _divisions;
        private readonly ILogger<DivisionRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public DivisionRepository(LeagueDbContext context, ILogger<DivisionRepository> logger)
            : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _divisions = context.Set<Division>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets divisions belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of divisions in the specified league</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the league is not found</exception>
        public async Task<IEnumerable<Division>> GetDivisionsByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching divisions by League ID: {LeagueId}", leagueId);
            var league =
                await _context
                    .Leagues
                    .Include(league => league.Conferences)
                    .ThenInclude(conference => conference.Divisions)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(league => league.Id == leagueId, cancellationToken)
                ?? throw new KeyNotFoundException($"League with ID {leagueId} not found.");

            return league?.Conferences.SelectMany(c => c.Divisions) ?? [];
        }

        /// <summary>
        /// Gets divisions belonging to a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of divisions in the specified conference</returns>
        public async Task<IEnumerable<Division>> GetDivisionsByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching divisions by Conference ID: {ConferenceId}", conferenceId);

            return await _divisions
                .AsNoTracking()
                .Where(d => d.ConferenceId == conferenceId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the division for a specific team
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>The division the team belongs to</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the team or its division is not found</exception>
        public async Task<Division> GetDivisionByTeamIdAsync(
            int teamId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching division by Team ID: {TeamId}", teamId);

            var team = await _context
                .Teams
                .Include(team => team.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(team => team.Id == teamId, cancellationToken);

            if (team is null)
            {
                _logger.LogWarning("Team with ID {TeamId} not found.", teamId);
                throw new KeyNotFoundException($"Team with ID {teamId} not found.");
            }
            else if (team.Division is null)
            {
                _logger.LogWarning("Division for Team ID {TeamId} not found.", teamId);
                throw new KeyNotFoundException($"Division for Team with ID {teamId} not found.");
            }

            return team.Division;
        }
    }
}
