namespace League.Server.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a team in the league
    /// </summary>
    public class TeamDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the team
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the division this team belongs to
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// Gets or sets the team's location (city or region)
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the team's name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the team's abbreviated name
        /// </summary>
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the number of games won
        /// </summary>
        public int Win { get; set; }

        /// <summary>
        /// Gets or sets the number of games lost
        /// </summary>
        public int Loss { get; set; }

        /// <summary>
        /// Gets or sets the number of games tied
        /// </summary>
        public int Tie { get; set; }

        /// <summary>
        /// Gets or sets the total points scored by the team
        /// </summary>
        public int PointsFor { get; set; }

        /// <summary>
        /// Gets or sets the total points scored against the team
        /// </summary>
        public int PointsAgainst { get; set; }

        /// <summary>
        /// Gets or sets the name of the team's home stadium
        /// </summary>
        public string? Stadium { get; set; }

        /// <summary>
        /// Gets or sets the stadium's seating capacity
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the stadium's street address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the stadium's city location
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the stadium's state location
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the stadium's ZIP code
        /// </summary>
        public string? Zip { get; set; }

        /// <summary>
        /// Gets or sets the stadium's latitude coordinate
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the stadium's longitude coordinate
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the list of players on the team
        /// </summary>
        public List<PlayerDto> Players { get; set; } = [];
    }
}
