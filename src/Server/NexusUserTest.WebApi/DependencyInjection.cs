using Microsoft.OpenApi.Models;
using NexusUserTest.Application;
using NexusUserTest.Infrastructure;
using Serilog;
using System.Reflection;

namespace NexusUserTest.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSerilog();
            services.AddApplication();
            services.AddInfrastructure(configuration);
            services.AddCorsConfiguration();
            services.AddSwagger();
            services.AddExceptionHandler();

            return services;
        }

        private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7183;http://localhost:5288")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("nexustest", new OpenApiInfo
                {
                    Title = "NexusTest API",
                    Version = "0.1",
                    Description = "Description of NexusTest API",
                    TermsOfService = new Uri("https://nexustest/privacy-policy"),
                    Contact = new OpenApiContact
                    {
                        Name = "NexusTest",
                        Email = "sarnaut@mail.com",
                        Url = new Uri("https://nexustest/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "NexusTest License",
                        Url = new Uri("https://nexustest/about-us")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        private static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
