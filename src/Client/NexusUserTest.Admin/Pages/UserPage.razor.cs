using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
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

        private NexusTableGrid<UserAdminDTO>? NexusTable;
        private readonly NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private readonly NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private UserAdminDTO? Upsert;
        private ApiResponse<List<UserAdminDTO>>? ApiResponse;
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
            => ApiResponse = await ServiceAPI!.UserService.GetAllUser("GroupUser");

        public async Task Insert()
        {
            if (await FillSelecItems())
            {
                Upsert = new UserAdminDTO { GroupUserItems = [] };
                IsUpsertForm = true;
            }
        }

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0 && await FillSelecItems())
            {
                Upsert = NexusTable.SelectedRows.First();
                IsUpsertForm = true;
            }
        }

        public async Task<bool> FillSelecItems()
        {
            var selects = await ServiceAPI!.GroupService.GetGroupSelect();
            if (!selects.Success)
            {
                NotificationService!.ShowError($"{selects.Error}", "Ошибка");
                return false;
            }
            GroupSelects = selects.Data;
            return true;
        }

        public async Task Save(UserAdminDTO entity)
        {
            if (entity.Id != 0)
                await Update(entity);
            else
                await Add(entity);
            await NexusTable!.Reload();
            IsUpsertForm = false;
        }

        public async Task Add(UserAdminDTO entity)
        {
            var response = await ServiceAPI!.UserService.AddUser(entity, "GroupUser");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.Add(response.Data!);
                await NexusTable.SelectRow(response.Data!);
                NotificationService!.ShowSuccess("Пользователь добавлен", "Успех");
            }
        }

        public async Task Update(UserAdminDTO entity)
        {
            var response = await ServiceAPI!.UserService.UpdateUser(entity, "GroupUser");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == response.Data!.Id);
                if (index >= 0)
                    NexusTable.Data[index] = response.Data!;
                await NexusTable.SelectRow(response.Data!);
                await NexusTable.CancelEditRow(response.Data!);
                NotificationService!.ShowSuccess("Пользователь изменен", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление пользователя", $"Вы уверены, что хотите удалить \"{data.FullName}\" пользователя?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.UserService.DeleteUser(data.Id);
                    NexusTable.RemoveRow(data);
                    NotificationService!.ShowSuccess("Пользователь удален", "Успех");
                }
            }
        }

        public void Cancel() => IsUpsertForm = false;
    }
}
