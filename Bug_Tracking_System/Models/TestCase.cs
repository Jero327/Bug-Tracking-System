using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public enum Status
    {
        ToDo, InProgress, Done
    }
    
    public class TestCase
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }        
        public int CaseTesterId { get; set; }
        
        [Required]
        public string TestCaseName { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string Image { get; set; }
        
        [Display(Name = "Create Time")]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }

        [Display(Name = "Modify Time")]
        [DataType(DataType.Date)]
        public DateTime ModifyTime { get; set; }

        [Display(Name = "Close Time")]
        [DataType(DataType.Date)]
        public DateTime CloseTime { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public Project Project { get; set; }

        public User CaseTester { get; set; }
    }
}