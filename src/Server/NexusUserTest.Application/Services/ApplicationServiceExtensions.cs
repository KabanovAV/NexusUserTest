using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace NexusUserTest.Application.Services
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ConfigurateCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7113;http://localhost:5168")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        public static IServiceCollection ConfigurateSwaggerGen(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Version 1.0", new OpenApiInfo
                {
                    Version = "Version 1.0",
                    Title = "Web API",
                    Description = "Try repeat what I learn from DotNetTutorials"
                });
            });

        public static IServiceCollection ConfigurateAutoMapper(this IServiceCollection services, Assembly[] assembly)
            => services.AddAutoMapper(assembly);

        public static IServiceCollection ConfigurateRepositoryService(this IServiceCollection services)
            => services.AddScoped<IRepoServiceManager, RepoServiceManager>();
    }
}
