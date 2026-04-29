using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NexusUserTest.Application.Common;
using NexusUserTest.Application.Services;

namespace NexusUserTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.ConfigurateCors().ConfigurateSwaggerGen().AddServices();

        private static IServiceCollection ConfigurateCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:7113;http://localhost:5168")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        private static IServiceCollection ConfigurateSwaggerGen(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Version 1.0", new OpenApiInfo
                {
                    Version = "Version 1.0",
                    Title = "Web API",
                    Description = "Try repeat what I learn from DotNetTutorials"
                });
            });

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupUserService, GroupUserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }            

        //public static IServiceCollection ConfigurateAutoMapper(this IServiceCollection services, Assembly[] assembly)
        //    => services.AddAutoMapper(assembly);

        //public static IServiceCollection ConfigurateRepositoryService(this IServiceCollection services)
        //    => services.AddScoped<IRepoServiceManager, RepoServiceManager>();
    }
}
