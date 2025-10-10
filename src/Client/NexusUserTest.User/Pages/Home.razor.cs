using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.User.Pages
{
    public partial class Home
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public NavigationManager? Navigation { get; set; }
        [Inject]
        public AuthenticationAPIService? AuthService { get; set; }
        [Inject]
        public UserSessionService? UserSession { get; set; }

        private ApiResponse<UserInfoTestDTO>? UserInfo;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadData()
            => UserInfo = await ServiceAPI!.UserService.GetUserTestInfo(UserSession!.UserId, "GroupUser.Group,GroupUser.Results.Answer");

        private void OpenTest(int groupUserId)
            => Navigation!.NavigateTo($"/test/{groupUserId}");

        private async Task Logout()
            => await AuthService!.LogoutAsync();
    }
}