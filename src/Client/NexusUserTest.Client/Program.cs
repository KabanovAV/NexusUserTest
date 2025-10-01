using NexusUserTest.Client.Components;
using NexusUserTest.Shared.Services;
using Serilog;

namespace NexusUserTest.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: false, reloadOnChange: true)
                   .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {              
                Log.Information("Starting web application");

                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();

                builder.Services.AddSerilog();
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
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
