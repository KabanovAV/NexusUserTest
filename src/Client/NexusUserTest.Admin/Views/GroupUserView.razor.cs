using Microsoft.AspNetCore.Components;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Views
{
    public partial class GroupUserView
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Parameter]
        public GroupInfoDetailsDTO? GroupInfo { get; set; }

        private NexusTableGrid<GroupUserDTO>? NexusTable;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;
        int hour, minute;

        protected override void OnParametersSet()
        {
            if (GroupInfo != null)
            {
                hour = GroupInfo.Setting.Timer.Hours;
                minute = GroupInfo.Setting.Timer.Minutes;
            }
        }

        private async void ChangeStatus(GroupUserDTO user, ChangeEventArgs e)
        {
            user.Status = Int32.Parse(e.Value.ToString());
            await ServiceAPI!.GroupUserService.UpdateGroupUser(user, "User");
        }

        private async void ChangeTimer()
        {
            GroupInfo.Setting.Timer = new TimeSpan(hour, minute, 0);
        }

        private async void SubmitSetting()
        {
            if (GroupInfo.Setting.Id == 0)
            {
                GroupInfo.Setting.GroupId = GroupInfo.Id;
                GroupInfo.Setting = await ServiceAPI.SettingService.AddSetting(GroupInfo.Setting);
            }
            else
                await ServiceAPI.SettingService.UpdateSetting(GroupInfo.Setting);
        }
    }
}
