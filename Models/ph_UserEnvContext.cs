using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ph_UserEnv.Models
{
    public class ph_UserEnvContext : DbContext
    {
        public ph_UserEnvContext(DbContextOptions<ph_UserEnvContext> options)
            : base(options)
        {
        }

        public DbSet<Message> messages { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
    }
}