using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class TopicPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<TopicDTO>? NexusTable;
        private NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private ApiResponse<List<TopicDTO>>? ApiResponse;
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
            => ApiResponse = await ServiceAPI!.TopicService.GetAllTopic("Specialization");

        public async Task Insert()
        {
            if (await FillSelecItems())
                await NexusTable!.InsertRow(new TopicDTO());
        }

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0 && await FillSelecItems())
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

        public async Task Add(TopicDTO item)
        {
            var response = await ServiceAPI!.TopicService.AddTopic(item);
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.Add(response.Data!);
                await NexusTable.SelectRow(response.Data!);
                NotificationService!.ShowSuccess("Тема добавлена", "Успех");
            }
        }

        public async Task Update(TopicDTO item)
        {
            var response = await ServiceAPI!.TopicService.UpdateTopic(item);
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == item.Id);
                if (index >= 0)
                    NexusTable.Data[index] = item;
                await NexusTable.SelectRow(item);
                await NexusTable.CancelEditRow(item);
                NotificationService!.ShowSuccess("Тема изменена", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление темы", $"Вы уверены, что хотите удалить \"{data.Title}\" тему?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    var response = await ServiceAPI!.TopicService.DeleteTopic(data.Id);
                    if (!response.Success)
                        NotificationService!.ShowError($"{response.Error}", "Ошибка");
                    else
                    {
                        NexusTable.RemoveRow(data);
                        NotificationService!.ShowSuccess("Тема удалена", "Успех");
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
