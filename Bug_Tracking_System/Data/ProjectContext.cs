using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext (DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Project { get; set; }
    }
}