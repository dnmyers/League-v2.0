namespace League.Server.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a player in the league
    /// </summary>
    public class PlayerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the player
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the team this player belongs to
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the player's jersey number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the player's position on the team
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Gets or sets the player's full name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the player's height in inches
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the player's weight in pounds
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the player's age
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the player's birth date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the player's experience level in the league
        /// </summary>
        public string? Experience { get; set; }

        /// <summary>
        /// Gets or sets the year the player was drafted
        /// </summary>
        public int? DraftYear { get; set; }

        /// <summary>
        /// Gets or sets the round in which the player was drafted
        /// </summary>
        public int? DraftRound { get; set; }

        /// <summary>
        /// Gets or sets the pick number within the draft round
        /// </summary>
        public int? DraftPick { get; set; }

        /// <summary>
        /// Gets or sets the player's college or university
        /// </summary>
        public string? College { get; set; }

        /// <summary>
        /// Gets or sets the state where the player's college is located
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the player's rank within their position
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// Gets or sets the player's overall rating
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Gets or sets the player's depth chart position
        /// </summary>
        public int? Depth { get; set; }
    }
}
