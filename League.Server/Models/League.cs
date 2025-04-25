using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace League.Server.Models
{
    /// <summary>
    /// Represents a sports league organization
    /// </summary>
    public class League
    {
        /// <summary>
        /// Database primary key
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier for the league
        /// </summary>
        [Required(ErrorMessage = "LeagueId is required")]
        [Display(Name = "League ID")]
        public int LeagueId { get; set; }

        /// <summary>
        /// Name of the league
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "League Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Abbreviation for the league name
        /// </summary>
        [Required(ErrorMessage = "Abbreviation is required")]
        [Display(Name = "Abbreviation")]
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Collection of conferences in this league
        /// </summary>
        [InverseProperty("League")]
        public List<Conference> Conferences { get; set; } = [];
    }
}
