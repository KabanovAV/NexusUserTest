using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class UserPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<UserDTO>? NexusTable;
        private readonly NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private readonly NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private UserDTO? Data;
        private List<UserDTO>? Items;
        private IEnumerable<SelectItem>? GroupSelects;

        private bool IsUpsertForm;
        public bool IsCrud => NexusTable != null
            && (NexusTable.InsertedItems.Count > 0 || NexusTable.EditedItems.Count > 0);
        public bool IsSelected => IsCrud || !NexusTable!.IsRowsSelected;

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
            var u = await ServiceAPI!.UserService.GetAllUser("GroupUser");
            Items = [.. u];
        }

        public async Task Insert()
        {
            GroupSelects = await ServiceAPI!.GroupService.GetGroupSelect();
            Data = new UserDTO { GroupUserItems = [] };
            IsUpsertForm = true;
        }

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                GroupSelects = await ServiceAPI!.GroupService.GetGroupSelect();
                Data = NexusTable.SelectedRows.First();
                IsUpsertForm = true;
            }
        }

        public async Task Save(UserDTO entity)
        {
            if (entity.Id != 0)
                await Update(entity);
            else
                await Add(entity);
            await NexusTable!.Reload();
            IsUpsertForm = false;
        }

        public async Task Add(UserDTO entity)
        {
            Data = await ServiceAPI!.UserService.AddUser(entity, "GroupUser");
            if (Data != null)
            {
                NexusTable!.Data.Add(Data);
                await NexusTable.SelectRow(Data);
                NotificationService!.ShowSuccess("Пользователь добавлен", "Успех");
            }
        }

        public async Task Update(UserDTO entity)
        {
            Data = await ServiceAPI!.UserService.UpdateUser(entity, "GroupUser");
            if (Data != null)
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == Data.Id);
                if (index >= 0)
                    NexusTable.Data[index] = Data;
                await NexusTable.SelectRow(Data);
                await NexusTable.CancelEditRow(Data);
                NotificationService!.ShowSuccess("Пользователь изменен", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                Data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление пользователя", $"Вы уверены, что хотите удалить \"{Data.FullName}\" пользователя?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.UserService.DeleteUser(Data.Id);
                    NexusTable.RemoveRow(Data);
                    NotificationService!.ShowSuccess("Пользователь удален", "Успех");
                }
            }
        }

        public async Task Cancel()
        {
            IsUpsertForm = false;
            await NexusTable!.CancelEditRow(Data!);
        }
    }
}
