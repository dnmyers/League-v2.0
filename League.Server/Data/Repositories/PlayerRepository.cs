using System.Linq.Expressions;
using League.Server.Data.Repositories.Interfaces;
using League.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Server.Data.Repositories
{
    /// <summary>
    /// Repository for managing player data in the database
    /// </summary>
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<Player> _players;
        private readonly ILogger<PlayerRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public PlayerRepository(LeagueDbContext context, ILogger<PlayerRepository> logger)
            : base(context, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _players = context.Set<Player>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets players by their team ID
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players belonging to the specified team</returns>
        public async Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(
            int teamId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Getting players for team ID {TeamId}...", teamId);
            Expression<Func<Player, bool>> predicate = player => player.TeamId == teamId;
            return await GetByPredicateAsync(predicate);
        }

        /// <summary>
        /// Gets players from a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified league</returns>
        public async Task<IEnumerable<Player>> GetPlayersByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching players by League ID: {LeagueId}", leagueId);

            return await _players
                .Include(p => p.Team)
                .ThenInclude(t => t!.Division)
                .ThenInclude(d => d!.Conference)
                .AsNoTracking()
                .Where(
                    p =>
                        p.Team != null
                        && p.Team.Division != null
                        && p.Team.Division.Conference != null
                        && p.Team.Division.Conference.LeagueId == leagueId
                )
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets players from a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified conference</returns>
        public async Task<IEnumerable<Player>> GetPlayersByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching players by Conference ID: {ConferenceId}", conferenceId);

            return await _players
                .Include(p => p.Team)
                .ThenInclude(t => t!.Division)
                .AsNoTracking()
                .Where(
                    p =>
                        p.Team != null
                        && p.Team.Division != null
                        && p.Team.Division.ConferenceId == conferenceId
                )
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets players from a specific division
        /// </summary>
        /// <param name="divisionId">The ID of the division</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified division</returns>
        public async Task<IEnumerable<Player>> GetPlayersByDivisionIdAsync(
            int divisionId,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Fetching players by Division ID: {DivisionId}", divisionId);

            return await _players
                .Include(p => p.Team)
                .AsNoTracking()
                .Where(p => p.Team != null && p.Team.DivisionId == divisionId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets players by their position
        /// </summary>
        /// <param name="position">The position to filter by (e.g., QB, RB, WR)</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players with the specified position</returns>
        public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(
            string position,
            CancellationToken cancellationToken = default
        )
        {
            _logger.LogDebug("Getting players by position: {Position}", position);
            Expression<Func<Player, bool>> predicate = player => player.Position == position;
            return await GetByPredicateAsync(predicate);
        }
    }
}
