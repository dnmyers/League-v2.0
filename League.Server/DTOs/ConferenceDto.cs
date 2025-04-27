namespace League.Server.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a conference in the league
    /// </summary>
    public class ConferenceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the conference
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the league this conference belongs to
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the full name of the conference
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the conference
        /// </summary>
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the list of divisions within this conference
        /// </summary>
        public List<DivisionDto> Divisions { get; set; } = [];
    }
}
