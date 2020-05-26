using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Bug_Tracking_System.Models
{
    public enum Role
    {
        Tester, TestManager, Developer, RDManager
    }
    
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public int Phone { get; set; }
        
        [Required]
        public string Email { get; set; }
        public Role Role { get; set; }

        [InverseProperty("Tester")]
        public ICollection<Bug> Tester { get; set; }

        [InverseProperty("TestManager")]
        public ICollection<Bug> TestManager { get; set; }

        [InverseProperty("Developer")]
        public ICollection<Bug> Developer { get; set; }

        public ICollection<TestCase> CaseTester { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}