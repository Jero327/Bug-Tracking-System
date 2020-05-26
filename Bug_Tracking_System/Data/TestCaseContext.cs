using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class TestCaseContext : DbContext
    {
        public TestCaseContext (DbContextOptions<TestCaseContext> options)
            : base(options)
        {
        }

        public DbSet<TestCase> TestCase { get; set; }
    }
}