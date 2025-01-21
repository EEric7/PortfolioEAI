using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioEAI.Models.Projects
{
    [Table("INDIVIDUALS")]
    public class Individual
    {
        [Key]
        [Column("IndividualID")]
        public int Id { get; set; }

        [StringLength(30)]
        public string? Post { get; set; }

        [Required(ErrorMessage = "Le rôle est requis")]
        [StringLength(30)]
        public string? Role { get; set; }

        [Required(ErrorMessage = "Le prenom est requis")]
        [Display(Name="First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name="Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Display(Name="Sa bio")]
        [StringLength(500)]
        public string? Resume { get; set; }
    }
}