using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Models;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for player repository operations
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Gets players by their team ID
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players belonging to the specified team</returns>
        Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(
            int teamId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets players from a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified league</returns>
        Task<IEnumerable<Player>> GetPlayersByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets players from a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified conference</returns>
        Task<IEnumerable<Player>> GetPlayersByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets players from a specific division
        /// </summary>
        /// <param name="divisionId">The ID of the division</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players from the specified division</returns>
        Task<IEnumerable<Player>> GetPlayersByDivisionIdAsync(
            int divisionId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets players by their position
        /// </summary>
        /// <param name="position">The position to filter by (e.g., QB, RB, WR)</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of players with the specified position</returns>
        Task<IEnumerable<Player>> GetPlayersByPositionAsync(
            string position,
            CancellationToken cancellationToken = default
        );
    }
}
