using System.Linq.Expressions;
using League.Server.Data.Repositories.Interfaces;
using League.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Server.Data.Repositories
{
    /// <summary>
    /// Repository for managing team data in the database
    /// </summary>
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<Team> _teams;
        private readonly ILogger<TeamRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public TeamRepository(LeagueDbContext context, ILogger<TeamRepository> logger)
            : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _teams = context.Set<Team>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets teams belonging to a specific division
        /// </summary>
        /// <param name="divisionId">The ID of the division</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified division</returns>
        public async Task<IEnumerable<Team>> GetTeamsByDivisionIdAsync(
            int divisionId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching teams by Division ID: {DivisionId}", divisionId);
            Expression<Func<Team, bool>> predicate = team => team.DivisionId == divisionId;
            return await GetByPredicateAsync(predicate);
        }

        /// <summary>
        /// Gets teams belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified league</returns>
        public async Task<IEnumerable<Team>> GetTeamsByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching teams by League ID: {LeagueId}", leagueId);
            return await _teams
                    .Include(team => team.Division)
                    .ThenInclude(division => division!.Conference)
                    // .ThenInclude(conference => conference!.League)
                    .AsNoTracking()
                    .Where(
                        team =>
                            team.Division != null
                            && team.Division.Conference != null
                            && team.Division.Conference.LeagueId == leagueId
                    )
                    .ToListAsync(cancellationToken) ?? [];
        }

        /// <summary>
        /// Gets teams belonging to a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified conference</returns>
        public async Task<IEnumerable<Team>> GetTeamsByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching teams by Conference ID: {ConferenceId}", conferenceId);

            return await _teams
                    .Include(team => team.Division)
                    .AsNoTracking()
                    .Where(
                        team => team.Division != null && team.Division.ConferenceId == conferenceId
                    )
                    .ToListAsync(cancellationToken) ?? [];
        }
    }
}
