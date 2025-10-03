using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Views
{
    public partial class GroupUserView
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        [Parameter]
        public GroupInfoDetailsDTO? GroupInfo { get; set; }

        private NexusTableGrid<GroupUserInfoAdminDTO>? NexusTable;
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

        private async void ChangeStatus(GroupUserInfoAdminDTO user, ChangeEventArgs e)
        {
            var status = Int32.Parse(e.Value.ToString());

            if (user.Status == 3)
            {
                var settings = new NexusDialogSetting("Внимание!", "Изменение статуса приведет к удалению результата пользователя. Вы уверены, что хотите изменить статус?", "Отменить", "Изменить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.ResultService.DeleteInfoResult(user.Id);
                    GroupInfo!.User.First(u => u == user).Results!.Clear();
                    await InvokeAsync(StateHasChanged);
                }
            }
            GroupUserUpdateDTO userUpdate = new() { Status = status };
            await ServiceAPI!.GroupUserService.UpdateGroupUser(user.Id, userUpdate);
        }

        private void ChangeTimer() => GroupInfo!.Setting.Timer = new TimeSpan(hour, minute, 0);

        private async void SubmitSetting()
        {
            if (GroupInfo!.Setting.Id == 0)
            {
                GroupInfo.Setting.GroupId = GroupInfo.Id;
                var response = await ServiceAPI!.SettingService.AddSetting(GroupInfo.Setting);
                if (!response.Success)
                    NotificationService!.ShowError($"{response.Error}", "Ошибка");
                else
                    GroupInfo.Setting = response.Data!;
            }
            else
                await ServiceAPI!.SettingService.UpdateSetting(GroupInfo.Setting);
        }
    }
}
