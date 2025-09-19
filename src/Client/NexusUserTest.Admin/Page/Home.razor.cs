using Microsoft.AspNetCore.Components;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Page
{
    public partial class Home
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private List<GroupInfoDTO>? Items;
        private List<GroupUserDTO>? Users;
        private GroupInfoDetailsDTO? GroupInfo;

        private NexusTableGrid<GroupUserDTO>? NexusTable;
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
        {
            var g = await ServiceAPI!.GroupService.GetAllInfoGroup("Specialization.Topics.TopicQuestion,GroupUser");
            Items = [.. g];
        }

        private async void DbClick(GroupInfoDTO item)
        {
            GroupInfo = await ServiceAPI!.GroupService.GetInfoDetailsGroup(item.Id, "Specialization.Topics.TopicQuestion,Setting,GroupUser.User");
            var u = await ServiceAPI!.GroupUserService.GetAllUsersByGroupId(item.Id, "User");
            Users = [.. u];
            IsShowGroupUsers = true;
            await InvokeAsync(StateHasChanged);
        }

        private void BackToGroups()
            => IsShowGroupUsers = false;
    }
}
