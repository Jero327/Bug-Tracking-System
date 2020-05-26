using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class EnrollmentContext : DbContext
    {
        public EnrollmentContext (DbContextOptions<EnrollmentContext> options)
            : base(options)
        {
        }

        public DbSet<Enrollment> Enrollment { get; set; }
    }
}