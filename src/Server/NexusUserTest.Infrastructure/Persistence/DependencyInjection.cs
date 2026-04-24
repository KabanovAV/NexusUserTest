using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;

namespace NexusUserTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services.AddDatabase(configuration)
                .AddRepository();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());
            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped<IRepository, Repository>();
    }
}
