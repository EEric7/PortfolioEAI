using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioEAI.Models.Projects
{
     [Table("PROJECTS")]
    public class Project
    {
        [Key]
        [Column("ProjectID")]
        public int Id { get; set; }

        [Display(Name="Nom du client")]
        [StringLength(50)]
        public string? Client { get; set; }

        [Required(ErrorMessage = "Le titre de la mission est requis")]
        [Display(Name="First Name")]
        [StringLength(50)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "La description de la mission est requis")]
        [Display(Name="First Name")]
        [StringLength(50)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<Job>? Jobs { get; set; }

        public ICollection<Individual>? Individuals { get; set; }

        public ICollection<string>? Tools { get; set; }
    }
}