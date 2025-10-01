using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class SpecializationPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<SpecializationDTO>? NexusTable;
        private readonly NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private readonly NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private ApiResponse<List<SpecializationDTO>>? ApiResponse;

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
            => ApiResponse = await ServiceAPI!.SpecializationService.GetAllSpecialization();

        public async Task Insert()
            => await NexusTable!.InsertRow(new SpecializationDTO());

        public void Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                if (EditMode == NexusTableGridEditMode.Multiple
                    && SelectMode == NexusTableGridSelectionMode.Multiple)
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
            NexusTable!.Refresh();
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

        public async Task Add(SpecializationDTO item)
        {
            var data = await ServiceAPI!.SpecializationService.AddSpecialization(item);
            if (data != null)
            {
                NexusTable!.Data.Add(data.Data);
                await NexusTable.SelectRow(data.Data);
                NotificationService!.ShowSuccess("Специализация добавлена", "Успех");
            }
        }

        public async Task Update(SpecializationDTO item)
        {
            var data = await ServiceAPI!.SpecializationService.UpdateSpecialization(item);
            if (data != null)
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == data.Data.Id);
                if (index >= 0)
                    NexusTable.Data[index] = data.Data;
                await NexusTable.SelectRow(data.Data);
                await NexusTable.CancelEditRow(data.Data);
                NotificationService!.ShowSuccess("Специализация изменена", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление специализации", $"Вы уверены, что хотите удалить \"{data.Title}\" специализацию?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    var isDeleted = await ServiceAPI!.SpecializationService.DeleteSpecialization(data.Id);
                    if (isDeleted.Data)
                    {
                        NexusTable.RemoveRow(data);
                        NotificationService!.ShowSuccess("Специализация удалена", "Успех");
                    }
                    else NotificationService!.ShowError("Удалить специализацию нельзя из-за наличия связей", "Ошибка");
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
