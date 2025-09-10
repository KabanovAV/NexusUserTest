using Microsoft.AspNetCore.Components;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Shared;
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

        private void DbClick(GroupDTO item)
            => NotificationService!.ShowSuccess($"{item.Title}", "Успех");
    }
}
