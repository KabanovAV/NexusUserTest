using NexusUserTest.Application.Services;
using NexusUserTest.Infrastructure;
using NexusUserTest.WebApi.Middlewares;
using Serilog;

namespace NexusUserTest.WebApi
{
    public class Startup(IConfiguration configuration)
    {
        private IConfiguration _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddEndpointsApiExplorer();
            services.AddSerilog();
            services.ConfigurateCors();
            services.ConfigurateSwaggerGen();
            services.ConfigurateAutoMapper(typeof(Startup).Assembly);
            services.ConfigurateDatabaseContext(_configuration);

            services.AddScoped<IRepoServiceManager, RepoServiceManager>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/Version 1.0/swagger.json", "Web API Version 1.0"));
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseNotFoundCustomMiddleware();
            app.UseLoggingCustomMiddleware();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
