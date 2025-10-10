using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;
using System.ComponentModel;
using System.Reflection;

namespace NexusUserTest.User.Pages
{
    public partial class Login
    {
        [Inject]
        public AuthenticationAPIService? AuthService { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }

        private EditContext? editContext;
        private LoginDto? loginDto;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            loginDto ??= new();
            editContext = new(loginDto);
        }

        private async Task LogIn()
        {
            if (editContext!.Validate())
            {
                var response = await AuthService!.LoginAsync(loginDto!);
                if (!response.Success)
                    NotificationService!.ShowError($"{response.Error}", "Ошибка");
            }                
        }

        private string GetDisplayName(string propertyName)
        {
            var property = typeof(LoginDto).GetProperty(propertyName);
            var displayAttribute = property?.GetCustomAttribute<DisplayNameAttribute>();
            return displayAttribute?.DisplayName ?? propertyName;
        }

    }
}
