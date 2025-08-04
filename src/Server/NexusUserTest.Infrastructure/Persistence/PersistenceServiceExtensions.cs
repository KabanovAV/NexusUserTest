using Microsoft.Extensions.DependencyInjection;
using NexusUserTest.Domain.Repositories;
using NexusUserTest.Infrastructure;

namespace NexusUserTest.Application.Services
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection ConfigurateRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
