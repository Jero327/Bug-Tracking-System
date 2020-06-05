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
    
    public class Bug
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int? ProjectId { get; set; }

        [Display(Name = "SubProject")]
        public int? SubProjectId { get; set; }

        [Display(Name = "Test Case")]
        public int? TestCaseId { get; set; }

        [Display(Name = "Status")]
        public BugStatus BugStatus { get; set; }
        
        [Display(Name = "Severity")]
        public int Severity { get; set; }

        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Display(Name = "Tester")]
        public int? TesterId { get; set; }

        [Display(Name = "Test Manager")]
        public int? TestManagerId { get; set; }

        [Display(Name = "Developer")]
        public int? DeveloperId { get; set; }

        [Display(Name = "Bug")]
        public string BugName { get; set; }
        public string Comment { get; set; }
        public int Rating
        {
            get
            {
                return Severity*Priority;
            }
        }

        [Display(Name = "Create Time")]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }

        [Display(Name = "Modify Time")]
        [DataType(DataType.Date)]
        public DateTime ModifyTime { get; set; }

        [Display(Name = "Close Time")]
        [DataType(DataType.Date)]
        public DateTime CloseTime { get; set; }


        public virtual Project Project { get; set; }
        public virtual SubProject SubProject { get; set; }
        public virtual TestCase TestCase { get; set; }
        public virtual User Tester { get; set; }
        public virtual User TestManager { get; set; }
        public virtual User Developer { get; set; }
    }
}