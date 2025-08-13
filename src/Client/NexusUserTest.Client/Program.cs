using NexusUserTest.Client.Components;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.ConfigureHttpClient();
            builder.Services.ConfigureAPI();
            builder.Services.AddNexusBlazor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddAdditionalAssemblies([typeof(Admin._Imports).Assembly, typeof(User._Imports).Assembly])
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
