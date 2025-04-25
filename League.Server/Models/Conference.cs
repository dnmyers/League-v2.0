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
        /// Foreign key to the league this conference belongs to
        /// </summary>
        [Required(ErrorMessage = "League ID is required")]
        public int LeagueId { get; set; }

        /// <summary>
        /// Name of the conference
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Conference Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Abbreviation for the conference name
        /// /// </summary>
        [StringLength(10, ErrorMessage = "Abbreviation cannot exceed 10 characters")]
        [Required(ErrorMessage = "Abbreviation is required")]
        [Display(Name = "Abbreviation")]
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Navigation property to the parent league
        /// </summary>
        [ForeignKey("LeagueId")]
        public League League { get; set; }

        /// <summary>
        /// Collection of divisions in this conference
        /// </summary>
        [InverseProperty("Conference")]
        public List<Division> Divisions { get; set; }
    }
}
