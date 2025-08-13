using Microsoft.Extensions.DependencyInjection;
using NexusUserTest.Shared.Components;

namespace NexusUserTest.Shared.Services
{
    public static class NexusBlazorServiceExtensions
    {
        public static void AddNexusBlazor(this IServiceCollection services)
        {
            services.AddSingleton<INexusNotificationService, NexusNotificationService>();
            services.AddSingleton<INexusDialogService, NexusDialogService>();
        }
    }
}
