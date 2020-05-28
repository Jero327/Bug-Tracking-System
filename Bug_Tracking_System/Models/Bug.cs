using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public enum BugStatus
    {
        New, Declined, Open, Reopen, Fixed, Closed
    }

    public enum Severity
    {
        General, Medium, Serious
    }

    public enum Priority
    {
        First=1, Second=2, Third=3, Fourth=4, Fifth=5
    }
    
    public class Bug
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "SubProject")]
        public int SubProjectId { get; set; }

        [Display(Name = "Test Case")]
        public int TestCaseId { get; set; }

        [Display(Name = "Status")]
        public BugStatus BugStatus { get; set; }
        
        [Display(Name = "Severity")]
        public Severity Severity { get; set; }

        [Display(Name = "Priority")]
        public Priority Priority { get; set; }

        [Display(Name = "Tester")]
        public int TesterId { get; set; }

        [Display(Name = "Test Manager")]
        public int TestManagerId { get; set; }

        [Display(Name = "Developer")]
        public int DeveloperId { get; set; }

        [Display(Name = "Bug")]
        public string BugName { get; set; }
        public string Comment { get; set; }
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


        public Project Project { get; set; }
        public SubProject SubProject { get; set; }
        public TestCase TestCase { get; set; }
        public User Tester { get; set; }
        public User TestManager { get; set; }
        public User Developer { get; set; }
    }
}