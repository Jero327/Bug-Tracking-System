using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Data
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}