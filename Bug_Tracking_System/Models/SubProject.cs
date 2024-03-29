using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public class SubProject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "SubProject Name")]
        public string SubProjectName { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Cteate Time")]
        [DataType(DataType.Date)]
        public DateTime CteateTime { get; set; }

        [Display(Name = "Finish Time")]
        [DataType(DataType.Date)]
        public DateTime FinishTime { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<TestCase> TestCases { get; set; }
    }
}