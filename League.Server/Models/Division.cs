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
        /// Unique identifier for the division
        /// </summary>
        [Required(ErrorMessage = "DivisionId is required")]
        [StringLength(50, ErrorMessage = "Division ID cannot exceed 50 characters")]
        [Display(Name = "Division ID")]
        public string? DivisionId { get; set; }

        /// <summary>
        /// Foreign key to the conference this division belongs to
        /// </summary>
        [Required(ErrorMessage = "ConferenceId is required")]
        [StringLength(50, ErrorMessage = "Conference ID cannot exceed 50 characters")]
        [Display(Name = "Conference ID")]
        public string? ConferenceId { get; set; }

        /// <summary>
        /// Name of the division
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Division Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Navigation property to the parent conference
        /// </summary>
        [ForeignKey("ConferenceId")]
        public Conference Conference { get; set; } = new();
    }
}
