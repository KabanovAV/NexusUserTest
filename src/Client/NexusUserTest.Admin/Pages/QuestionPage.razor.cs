using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class QuestionPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<QuestionAdminDTO>? NexusTable;
        private NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private QuestionAdminDTO? Upsert;
        private ApiResponse<List<QuestionAdminDTO>>? ApiResponse;
        private IEnumerable<SelectItem>? TopicSelects;

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
            => ApiResponse = await ServiceAPI!.QuestionService.GetAllQuestion("Answers,TopicQuestion");

        public async Task Insert()
        {
            if (await FillSelecItems())
            {
                Upsert = new QuestionAdminDTO { AnswerItems = [], TopicQuestionItems = [] };
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
            var selects = await ServiceAPI!.TopicService.GetTopicSelect();
            if (!selects.Success)
            {
                NotificationService!.ShowError($"{selects.Error}", "Ошибка");
                return false;
            }
            TopicSelects = selects.Data;
            return true;
        }

        public async Task Save(QuestionAdminDTO entity)
        {
            if (entity.Id != 0)
                await Update(entity);
            else
                await Add(entity);
            await NexusTable!.Reload();
            IsUpsertForm = false;
        }

        public async Task Add(QuestionAdminDTO entity)
        {
            var response = await ServiceAPI!.QuestionService.AddQuestion(entity, "Answers,TopicQuestion");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                NexusTable!.Data.Add(response.Data!);
                await NexusTable.SelectRow(response.Data!);
                await NexusTable.OnExpandRow(response.Data!);
                NotificationService!.ShowSuccess("Вопрос добавлен", "Успех");
            }
        }

        public async Task Update(QuestionAdminDTO entity)
        {
            var response = await ServiceAPI!.QuestionService.UpdateQuestion(entity, "Answers,TopicQuestion");
            if (!response.Success)
                NotificationService!.ShowError($"{response.Error}", "Ошибка");
            else
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == entity.Id);
                if (index >= 0)
                    NexusTable.Data[index] = entity;
                await NexusTable.SelectRow(entity);
                await NexusTable.CancelEditRow(entity);
                NotificationService!.ShowSuccess("Вопрос изменен", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление вопроса", $"Вопрос удалится вместе с ответами. Вы уверены, что хотите удалить \"{data.Title}\" вопрос?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.QuestionService.DeleteQuestion(data.Id);
                    NexusTable.RemoveRow(data);
                    NotificationService!.ShowSuccess("Вопрос удален", "Успех");
                }
            }
        }

        public void Cancel() => IsUpsertForm = false;
    }
}
