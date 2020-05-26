using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class BugContext : DbContext
    {
        public BugContext (DbContextOptions<BugContext> options)
            : base(options)
        {
        }

        public DbSet<Bug> Bug { get; set; }
    }
}