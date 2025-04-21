using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace League.Server.Models
{
    /// <summary>
    /// Represents a conference in a sports league
    /// </summary>
    public class Conference
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the conference
        /// </summary>
        [Required(ErrorMessage = "ConferenceId is required")]
        [StringLength(50, ErrorMessage = "Conference ID cannot exceed 50 characters")]
        [Display(Name = "Conference ID")]
        public string? ConferenceId { get; set; }

        /// <summary>
        /// Foreign key to the league this conference belongs to
        /// </summary>
        [Required(ErrorMessage = "LeagueId is required")]
        [StringLength(50, ErrorMessage = "League ID cannot exceed 50 characters")]
        [Display(Name = "League ID")]
        public string? LeagueId { get; set; }

        /// <summary>
        /// Name of the conference
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Conference Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Navigation property to the parent league
        /// </summary>
        [ForeignKey("LeagueId")]
        [InverseProperty("Conferences")]
        public League League { get; set; } = new();
    }
}
