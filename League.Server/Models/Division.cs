using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace League.Server.Models
{
    /// <summary>
    /// Represents a division within a conference
    /// </summary>
    public class Division
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the conference this division belongs to
        /// </summary>
        [Required(ErrorMessage = "Conference ID is required")]
        public int ConferenceId { get; set; }

        /// <summary>
        /// Name of the division
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Division Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required")]
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Navigation property to the parent conference
        /// </summary>
        [ForeignKey("ConferenceId")]
        public Conference? Conference { get; set; }

        /// <summary>
        /// Collection of teams in this division
        /// </summary>
        [InverseProperty("Division")]
        public List<Team>? Teams { get; set; }
    }
}
