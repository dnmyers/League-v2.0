using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using League.Server.Models;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface for league repository operations
    /// </summary>
    public interface ILeagueRepository
    {
        /// <summary>
        /// Gets leagues containing the specified name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A collection of leagues matching the search criteria</returns>
        Task<IEnumerable<Models.League>> GetLeaguesByNameAsync(
            string name,
            CancellationToken cancellationToken = default
        );
    }
}
