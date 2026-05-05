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
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupUserService, GroupUserService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ITopicQuestionService, TopicQuestionService>();
            services.AddScoped<ITestSettingService, TestSettingService>();
            //services.AddScoped<ITestResultService, TestResultService>();

            return services;
        }

        private static IServiceCollection AddValidationService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidationService, ValidationService>();
            return services;
        }
    }
}
