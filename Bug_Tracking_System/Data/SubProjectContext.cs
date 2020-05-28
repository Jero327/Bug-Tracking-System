using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class SubProjectContext : DbContext
    {
        public SubProjectContext (DbContextOptions<SubProjectContext> options)
            : base(options)
        {
        }

        public DbSet<SubProject> SubProject { get; set; }
    }
}