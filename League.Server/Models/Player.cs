using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace League.Server.Models
{
    /// <summary>
    /// Represents a player in a sports team
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the team the player belongs to
        /// </summary>
        [Required(ErrorMessage = "Team ID is required")]
        public int TeamId { get; set; }

        /// <summary>
        /// Player's jersey number
        /// </summary>
        [Range(0, 99, ErrorMessage = "Number must be between 0 and 99")]
        public int Number { get; set; }

        /// <summary>
        /// Player's position on the team
        /// </summary>
        [StringLength(20, ErrorMessage = "Position cannot exceed 20 characters")]
        [Display(Name = "Position")]
        public string? Position { get; set; }

        /// <summary>
        /// Player's full name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Player Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Player's height in inches
        /// </summary>
        [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 inches")]
        [Display(Name = "Height (inches)")]
        public int? Height { get; set; } = 0;

        /// <summary>
        /// Player's weight in pounds
        /// </summary>
        [Range(0, 500, ErrorMessage = "Weight must be between 0 and 500 pounds")]
        [Display(Name = "Weight (lbs)")]
        public int? Weight { get; set; }

        /// <summary>
        /// Player's age in years
        /// </summary>
        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100 years")]
        public int? Age { get; set; }

        /// <summary>
        /// Player's birth date
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Player's professional experience
        /// </summary>
        [StringLength(50, ErrorMessage = "Experience cannot exceed 50 characters")]
        public string? Experience { get; set; }

        /// <summary>
        /// Year the player was drafted
        /// </summary>
        [Range(1900, 2100, ErrorMessage = "Draft year must be valid")]
        [Display(Name = "Draft Year")]
        public int? DraftYear { get; set; }

        /// <summary>
        /// Round in which the player was drafted
        /// </summary>
        [Range(0, 50, ErrorMessage = "Draft round must be between 0 and 50")]
        [Display(Name = "Draft Round")]
        public int? DraftRound { get; set; }

        /// <summary>
        /// Pick number in the draft
        /// </summary>
        [Range(0, 500, ErrorMessage = "Draft pick must be between 0 and 500")]
        [Display(Name = "Draft Pick")]
        public int? DraftPick { get; set; }

        /// <summary>
        /// College the player attended
        /// </summary>
        [StringLength(100, ErrorMessage = "College name cannot exceed 100 characters")]
        public string? College { get; set; }

        /// <summary>
        /// Player's home state
        /// </summary>
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters")]
        public string? State { get; set; }

        /// <summary>
        /// Player's rank
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Rank must be a positive number")]
        public int? Rank { get; set; }

        /// <summary>
        /// Player's rating
        /// </summary>
        [Range(0, 100, ErrorMessage = "Rating must be between 0 and 100")]
        public int? Rating { get; set; }

        /// <summary>
        /// Player's depth chart position
        /// </summary>
        [Range(0, 10, ErrorMessage = "Depth must be between 0 and 10")]
        public int? Depth { get; set; }

        /// <summary>
        /// Navigation property to the player's team
        /// </summary>
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }
    }
}
