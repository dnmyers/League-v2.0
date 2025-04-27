using System.ComponentModel.DataAnnotations;

namespace League.Server.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a league organization
    /// </summary>
    public class LeagueDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the league entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the league's business identifier
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the full name of the league
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the league
        /// </summary>
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the list of conferences in this league
        /// </summary>
        public List<ConferenceDto> Conferences { get; set; } = [];
    }
}
