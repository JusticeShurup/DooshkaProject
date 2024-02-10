using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Dooshka.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()) + "\\Dooshka.API")
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");

            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);

            builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
