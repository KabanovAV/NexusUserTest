using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NexusUserTest.Infrastructure;

namespace NexusUserTest.WebApi
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<DbDataContext>
    {
        public DbDataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<DbDataContext>()
                .UseNpgsql(configuration.GetConnectionString("PostgreConnection"),
                b => b.MigrationsAssembly("NexusUserTest.Infrastructure"));

            return new DbDataContext(builder.Options);
        }
    }
}
