using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Models;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for team repository operations
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Gets teams belonging to a specific division
        /// </summary>
        /// <param name="divisionId">The ID of the division</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified division</returns>
        Task<IEnumerable<Team>> GetTeamsByDivisionIdAsync(
            int divisionId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets teams belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified league</returns>
        Task<IEnumerable<Team>> GetTeamsByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets teams belonging to a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of teams in the specified conference</returns>
        Task<IEnumerable<Team>> GetTeamsByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        );
    }
}
