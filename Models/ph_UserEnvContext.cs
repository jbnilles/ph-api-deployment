using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ph_UserEnv.Models
{
    public class ph_UserEnvContext : IdentityDbContext<ApplicationUser>
    {
        public ph_UserEnvContext(DbContextOptions<ph_UserEnvContext> options)
            : base(options)
        {
        }

        public DbSet<Message> messages { get; set; }
    }
}