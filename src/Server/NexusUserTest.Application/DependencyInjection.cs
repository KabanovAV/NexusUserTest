using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NexusUserTest.Application.Common;
using NexusUserTest.Application.Services;
using System.Reflection;

namespace NexusUserTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddServices().AddValidationService();

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IGroupUserService, GroupUserService>();
            services.AddScoped<IQuestionService, QuestionService>();
            //services.AddScoped<ITestResultService, TestResultService>();
            services.AddScoped<ITestSettingService, TestSettingService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static IServiceCollection AddValidationService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidationService, ValidationService>();
            return services;
        }

        //public static IServiceCollection ConfigurateAutoMapper(this IServiceCollection services, Assembly[] assembly)
        //    => services.AddAutoMapper(assembly);

        //public static IServiceCollection ConfigurateRepositoryService(this IServiceCollection services)
        //    => services.AddScoped<IRepoServiceManager, RepoServiceManager>();
    }
}
