namespace League.Server.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a division within a conference
    /// </summary>
    public class DivisionDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the division
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the conference this division belongs to
        /// </summary>
        public int ConferenceId { get; set; }

        /// <summary>
        /// Gets or sets the full name of the division
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the division
        /// </summary>
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the list of teams in this division
        /// </summary>
        public List<TeamDto> Teams { get; set; } = [];
    }
}
