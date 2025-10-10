using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusUserTest.Application.Services;
using NexusUserTest.Infrastructure;
using NexusUserTest.WebApi.Middlewares;
using Serilog;
using System.Text;

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
            services.ConfigurateAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<DbDataContext>();

            var connection = _configuration.GetConnectionString("PostgreConnection");
            services.AddDbContext<DbDataContext>(options => options.UseNpgsql(connection));

            var jwtSettings = _configuration.GetSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            services.ConfigurateRepositoryManager();
            services.ConfigurateRepositoryService();
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
