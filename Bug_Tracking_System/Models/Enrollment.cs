using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }       
        public int UserId { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}