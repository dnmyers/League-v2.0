using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Models;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for division repository operations
    /// </summary>
    public interface IDivisionRepository
    {
        /// <summary>
        /// Gets divisions belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of divisions in the specified league</returns>
        Task<IEnumerable<Division>> GetDivisionsByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets divisions belonging to a specific conference
        /// </summary>
        /// <param name="conferenceId">The ID of the conference</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of divisions in the specified conference</returns>
        Task<IEnumerable<Division>> GetDivisionsByConferenceIdAsync(
            int conferenceId,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Gets the division for a specific team
        /// </summary>
        /// <param name="teamId">The ID of the team</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>The division the team belongs to</returns>
        Task<Division> GetDivisionByTeamIdAsync(
            int teamId,
            CancellationToken cancellationToken = default
        );
    }
}
