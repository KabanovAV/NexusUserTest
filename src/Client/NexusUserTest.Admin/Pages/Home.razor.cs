using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class Home
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private ApiResponse<List<GroupInfoDTO>>? Groups;
        private ApiResponse<GroupInfoDetailsDTO>? GroupInfo;
        private bool IsShowGroupUsers;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadData()
            => Groups = await ServiceAPI!.GroupService.GetAllInfoGroup("Specialization.Topics.TopicQuestion,GroupUser");

        private async void DbClick(GroupInfoDTO item)
        {
            GroupInfo = await ServiceAPI!.GroupService.GetInfoDetailsGroup(item.Id, "Specialization.Topics.TopicQuestion,Setting,GroupUser.User,GroupUser.Results.Question.Answers");
            IsShowGroupUsers = true;
            await InvokeAsync(StateHasChanged);
        }

        private void BackToGroups()
        {
            IsShowGroupUsers = false;
            GroupInfo = null;
        }
    }
}
