using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioEAI.Models.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string? Client { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<Task>? Tasks { get; set; }

        public List<People>? Peoples { get; set; }

        public List<string>? Tools { get; set; }
    }
}