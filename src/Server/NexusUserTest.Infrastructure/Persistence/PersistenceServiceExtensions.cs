using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NexusUserTest.Infrastructure
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection ConfigurateDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("PostgreConnection");
            services.AddDbContext<DbDataContext>(options =>
                options.UseNpgsql(connection, x => x.MigrationsAssembly("NexusUserTest.Infrastructure.Persistence")));
            return services;
        }
    }
}
