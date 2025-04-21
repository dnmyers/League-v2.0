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
        [StringLength(50, ErrorMessage = "League ID cannot exceed 50 characters")]
        [Display(Name = "League ID")]
        public string? LeagueId { get; set; }

        /// <summary>
        /// Name of the league
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "League Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Collection of conferences in this league
        /// </summary>
        [InverseProperty("League")]
        public List<Conference> Conferences { get; set; } = [];
    }
}
