using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class GroupPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<GroupEditDTO>? NexusTable;
        private readonly NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private readonly NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private ApiResponse<List<GroupEditDTO>>? ApiResponse;
        private IEnumerable<SelectItem>? SpecializationSelects;

        public bool IsCrud => NexusTable != null
            && (NexusTable.InsertedItems.Count > 0 || NexusTable.EditedItems.Count > 0);
        public bool IsSelected => IsCrud || !NexusTable!.IsRowsSelected;
        public bool IsSaveCancel => !IsCrud;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadData()
            => ApiResponse = await ServiceAPI!.GroupService.GetAllEditGroup("Specialization,GroupUser");

        public async Task Insert()
        {
            if (await FillSelecItems())
                await NexusTable!.InsertRow(new GroupEditDTO { Begin = DateTime.Now, End = DateTime.Now });
        }

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0 && await FillSelecItems())
            {
                if (EditMode == NexusTableGridEditMode.Multiple && SelectMode == NexusTableGridSelectionMode.Multiple)
                {
                    foreach (var selectRow in NexusTable.SelectedRows)
                        NexusTable.EditRow(selectRow);
                }
                else
                {
                    var data = NexusTable.SelectedRows.First();
                    NexusTable.EditRow(data);
                }
            }
        }

        public async Task<bool> FillSelecItems()
        {
            var selects = await ServiceAPI!.SpecializationService.GetSpecializationSelect();
            if (!selects.Success)
            {
                NotificationService!.ShowError($"{selects.Error}", "Ошибка");
                return false;
            }
            SpecializationSelects = selects.Data;
            return true;
        }

        public async Task Save()
        {
            if (NexusTable!.InsertedItems.Count == 0 && NexusTable!.EditedItems.Count > 0)
            {
                var data = NexusTable!.EditedItems.First();
                await Update(data);
            }
            else
            {
                var data = NexusTable!.InsertedItems.First();
                await Add(data);
            }
            await NexusTable.Reload();
        }

        public async Task Add(GroupEditDTO entity)
        {
            var response = await ServiceAPI!.GroupService.AddGroup(entity, "Specialization,GroupUser");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.Add(response.Data!);
                await NexusTable.SelectRow(response.Data!);
                NotificationService!.ShowSuccess("Группа добавлена", "Успех");
            }
        }

        public async Task Update(GroupEditDTO entity)
        {
            var response = await ServiceAPI!.GroupService.UpdateGroup(entity, "Specialization,GroupUser");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == response.Data!.Id);
                if (index >= 0)
                    NexusTable.Data[index] = response.Data!;
                await NexusTable.SelectRow(response.Data!);
                await NexusTable.CancelEditRow(response.Data!);
                NotificationService!.ShowSuccess("Группа изменена", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление группы", $"Вы уверены, что хотите удалить \"{data.Title}\" группу?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    var response = await ServiceAPI!.GroupService.DeleteGroup(data.Id);
                    if (!response.Success)
                        NotificationService!.ShowError($"{response.Error}", "Ошибка");
                    else
                    {
                        NexusTable.RemoveRow(data);
                        NotificationService!.ShowSuccess("Группа удалена", "Успех");
                    }
                }
            }
        }

        public async Task Cancel()
        {
            var data = NexusTable!.SelectedRows.First();
            await NexusTable.CancelEditRow(data);
        }
    }
}
