using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioEAI.Models.Projects
{
     [Table("JOBS")]
    public class Job
    {
        [Key]
        [Column("JobID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La designiatiion de la tâche est requis")]
        [StringLength(30)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Le rôle est requis")]
        [StringLength(30)]
        public List<string>? Description { get; set; }
    }
}