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

        private NexusTableGrid<GroupDTO>? NexusTable;
        private List<GroupDTO>? Items;
        private IEnumerable<UserDTO>? Users;

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
            var g = await ServiceAPI!.GroupService.GetAllGroup("Specialization,GroupUser");
            Items = [.. g];
        }

        private async void DbClick(GroupDTO item)
        {
            //Users = await ServiceAPI!.UserService.GetAllUser("Specialization,GroupUser");
            //Users = Users.Where(x => x.
            NotificationService!.ShowSuccess($"{item.Title}", "Успех");
        } 
    }
}
