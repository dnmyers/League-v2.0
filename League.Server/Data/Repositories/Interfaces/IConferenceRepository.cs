using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Models;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for conference repository operations
    /// </summary>
    public interface IConferenceRepository
    {
        /// <summary>
        /// Gets conferences belonging to a specific league
        /// </summary>
        /// <param name="leagueId">The ID of the league</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of conferences in the specified league</returns>
        Task<IEnumerable<Conference>> GetConferencesByLeagueIdAsync(
            int leagueId,
            CancellationToken cancellationToken = default
        );
    }
}
