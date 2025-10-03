using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Views
{
    public partial class AnswerView
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }

        [Parameter]
        public List<AnswerAdminDTO>? Items { get; set; }
        [Parameter]
        public int QuestionId { get; set; }

        private NexusTableGrid<AnswerAdminDTO>? NexusTable;
        private NexusTableGridEditMode EditMode = NexusTableGridEditMode.Multiple;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        public bool IsCrud => NexusTable != null && NexusTable.InsertedItems.Count == 0 && NexusTable.EditedItems.Count > 0;
        public bool IsSelected => !NexusTable.IsRowsSelected && NexusTable.InsertedItems.Count == 0 || NexusTable.EditedItems.Count > 0;
        public bool IsSaveCancel => NexusTable.InsertedItems.Count == 0 && NexusTable.EditedItems.Count == 0;

        public async Task Insert()
            => await NexusTable!.InsertRow(new AnswerAdminDTO { QuestionId = QuestionId });

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
                if (NexusTable!.InsertedItems.Count > 1)
                    await AddRange([.. NexusTable!.EditedItems]);
                else
                {
                    var data = NexusTable!.InsertedItems.First();
                    await Add(data);
                }
            }
            await NexusTable.Reload();
        }

        public async Task Add(AnswerAdminDTO item)
        {
            var response = await ServiceAPI!.AnswerService.AddAnswer(item);
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.Add(response.Data!);
                await NexusTable.SelectRow(response.Data!);
            }
        }

        public async Task AddRange(List<AnswerAdminDTO> item)
        {
            var response = await ServiceAPI!.AnswerService.AddRangeAnswer(item);
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.AddRange(response.Data!);
                await NexusTable.SelectRow(response.Data!.Last());
            }
        }

        public async Task Update(AnswerAdminDTO item)
        {
            var response = await ServiceAPI!.AnswerService.UpdateAnswer(item);
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == item.Id);
                if (index >= 0)
                    NexusTable.Data[index] = item;
                await NexusTable.SelectRow(item);
                await NexusTable.CancelEditRow(item);
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var response = await ServiceAPI!.AnswerService.DeleteAnswer(data.Id);
                if (!response.Success)
                    NotificationService!.ShowError($"{response.Error}", "Ошибка");
                else
                    NexusTable.RemoveRow(data);
            }
        }

        public async Task Cancel()
        {
            var data = NexusTable!.SelectedRows.First();
            await NexusTable.CancelEditRow(data);
        }
    }
}
