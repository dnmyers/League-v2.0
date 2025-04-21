using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace League.Server.Models
{
    /// <summary>
    /// Represents a sports team
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the team
        /// </summary>
        [Required(ErrorMessage = "TeamId is required")]
        [StringLength(50, ErrorMessage = "Team ID cannot exceed 50 characters")]
        [Display(Name = "Team ID")]
        public string? TeamId { get; set; }

        /// <summary>
        /// Foreign key to the division this team belongs to
        /// </summary>
        [Required(ErrorMessage = "DivisionId is required")]
        [StringLength(50, ErrorMessage = "Division ID cannot exceed 50 characters")]
        [Display(Name = "Division ID")]
        public string? DivisionId { get; set; }

        /// <summary>
        /// City or geographic location of the team
        /// </summary>
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string? Location { get; set; }

        /// <summary>
        /// Team name
        /// </summary>
        [Required(ErrorMessage = "Team name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Team Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Number of games won
        /// </summary>
        [Range(0, 500, ErrorMessage = "Wins must be a positive number")]
        public int Win { get; set; }

        /// <summary>
        /// Number of games lost
        /// </summary>
        [Range(0, 500, ErrorMessage = "Losses must be a positive number")]
        public int Loss { get; set; }

        /// <summary>
        /// Number of games tied
        /// </summary>
        [Range(0, 500, ErrorMessage = "Ties must be a positive number")]
        public int Tie { get; set; }

        /// <summary>
        /// Total points scored by the team
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Points For must be a positive number")]
        [Display(Name = "Points For")]
        public int PointsFor { get; set; }

        /// <summary>
        /// Total points scored against the team
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Points Against must be a positive number")]
        [Display(Name = "Points Against")]
        public int PointsAgainst { get; set; }

        /// <summary>
        /// Name of the team's stadium
        /// </summary>
        [StringLength(100, ErrorMessage = "Stadium name cannot exceed 100 characters")]
        public string? Stadium { get; set; }

        /// <summary>
        /// Seating capacity of the stadium
        /// </summary>
        [Range(0, 500000, ErrorMessage = "Capacity must be a positive number")]
        public int Capacity { get; set; }

        /// <summary>
        /// Street address of the stadium
        /// </summary>
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        /// <summary>
        /// City where the stadium is located
        /// </summary>
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string? City { get; set; }

        /// <summary>
        /// State where the stadium is located
        /// </summary>
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string? State { get; set; }

        /// <summary>
        /// Postal code of the stadium location
        /// </summary>
        [StringLength(20, ErrorMessage = "Zip code cannot exceed 20 characters")]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }

        /// <summary>
        /// Latitude coordinate of the stadium location
        /// </summary>
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude coordinate of the stadium location
        /// </summary>
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double Longitude { get; set; }

        /// <summary>
        /// Navigation property to the division this team belongs to
        /// </summary>
        [ForeignKey("DivisionId")]
        public Division Division { get; set; } = new();

        /// <summary>
        /// Collection of players on this team
        /// </summary>
        [InverseProperty("Team")]
        public ICollection<Player> Players { get; set; } = [];
    }
}
