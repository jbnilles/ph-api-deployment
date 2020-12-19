using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ph_UserEnv.Models
{
    public class ph_UserEnvContextFactory : IDesignTimeDbContextFactory<ph_UserEnvContext>
    {

        ph_UserEnvContext IDesignTimeDbContextFactory<ph_UserEnvContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ph_UserEnvContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString);

            return new ph_UserEnvContext(builder.Options);
        }
    }
}