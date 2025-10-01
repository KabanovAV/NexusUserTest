using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared;
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

        private NexusTableGrid<QuestionDTO>? NexusTable;
        private NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private QuestionDTO? Data;
        private List<QuestionDTO>? Items;
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
        {
            var q = await ServiceAPI!.QuestionService.GetAllQuestion("Answers,TopicQuestion");
            Items = [.. q];
        }

        public async Task Insert()
        {
            TopicSelects = await ServiceAPI!.TopicService.GetTopicSelect();
            Data = new QuestionDTO { AnswerItems = [], TopicQuestionItems = [] };
            IsUpsertForm = true;
        }            

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                TopicSelects = await ServiceAPI!.TopicService.GetTopicSelect();
                Data = NexusTable.SelectedRows.First();
                IsUpsertForm = true;
            }
        }

        public async Task Save(QuestionDTO entity)
        {
            if (entity.Id != 0)
                await Update(entity);
            else
                await Add(entity);
            await NexusTable!.Reload();
            IsUpsertForm = false;
        }

        public async Task Add(QuestionDTO entity)
        {
            Data = await ServiceAPI!.QuestionService.AddQuestion(entity, "Answers,TopicQuestion");
            if (Data != null)
            {
                NexusTable!.Data.Add(Data);
                await NexusTable.SelectRow(Data);
                await NexusTable.OnExpandRow(Data);
                NotificationService!.ShowSuccess("Вопрос добавлен", "Успех");
            }
        }

        public async Task Update(QuestionDTO entity)
        {
            Data = await ServiceAPI!.QuestionService.UpdateQuestion(entity, "Answers,TopicQuestion");
            if (Data != null)
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == Data.Id);
                if (index >= 0)
                    NexusTable.Data[index] = Data;
                await NexusTable.SelectRow(Data);
                await NexusTable.CancelEditRow(Data);
                NotificationService!.ShowSuccess("Вопрос изменен", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                Data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление вопроса", $"Вопрос удалится вместе с ответами. Вы уверены, что хотите удалить \"{Data.Title}\" вопрос?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.QuestionService.DeleteQuestion(Data.Id);
                    NexusTable.RemoveRow(Data);
                    NotificationService!.ShowSuccess("Вопрос удален", "Успех");
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
