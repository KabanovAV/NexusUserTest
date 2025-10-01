using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace NexusUserTest.Shared.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("HttpClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7183");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        public static void ConfigureAPI(this IServiceCollection services)
        {
            services.AddScoped<IAPIService, APIService>();
            services.AddScoped<IApiResponseHandler, ApiResponseHandler>();
        }
    }
}
