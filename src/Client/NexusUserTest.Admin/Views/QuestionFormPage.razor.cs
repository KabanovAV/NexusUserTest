using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Shared;
using NexusUserTest.Shared.NexusBlazor;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NexusUserTest.Admin.Views
{
    public partial class QuestionFormPage
    {
        [Parameter, EditorRequired]
        public QuestionDTO Data { get; set; }
        [Parameter]
        public IEnumerable<SelectItem> TopicSelects { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public EventCallback<QuestionDTO> OnSave { get; set; }

        private NexusTableGrid<AnswerDTO>? NexusTable;
        private NexusTableGridEditMode EditMode = NexusTableGridEditMode.Multiple;
        private NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        public bool IsSelected => !NexusTable.IsRowsSelected;
        private EditContext? editContext;
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Data ??= new();
            editContext = new(Data);
        }
        private string GetDisplayName(string propertyName)
        {
            var property = typeof(QuestionDTO).GetProperty(propertyName);
            var displayAttribute = property?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? propertyName;
        }
        private void CheckboxChange(int args)
        {
            if (Data.TopicQuestionItems!.FirstOrDefault(x => x.TopicId == args) is TopicQuestionCreateDTO topic && topic != null)
                Data.TopicQuestionItems!.Remove(topic);
            else
            {
                var topicQuestion = new TopicQuestionCreateDTO { TopicId = args };
                Data.TopicQuestionItems!.Add(topicQuestion);
            }
        }
        public async Task AddAnwser()
            => await NexusTable!.InsertRow(new AnswerDTO());
        public void EditAnwser()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                if (EditMode == NexusTableGridEditMode.Multiple
                    && SelectMode == NexusTableGridSelectionMode.Multiple)
                {
                    foreach (var selectRow in NexusTable.SelectedRows)
                    {
                        NexusTable.EditRow(selectRow);
                    }
                }
                else
                {
                    var data = NexusTable.SelectedRows.First();
                    NexusTable.EditRow(data);
                }
            }
        }
        public async Task RemoveAnwser()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                await NexusTable.CancelEditRow(data);
            }
        }
        private async Task Save()
        {
            if (editContext!.Validate())
            {
                await OnSave.InvokeAsync(Data);
                if (Data.Id == 0)
                    Data.TopicQuestionItems!.Clear();
            }
        }
        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
            if (Data.Id == 0)
                Data.TopicQuestionItems!.Clear();
        }
    }
}
