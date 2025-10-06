using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;
using NexusUserTest.User.Views;

namespace NexusUserTest.User.Pages
{
    public partial class TestView
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }
        [Inject]
        public NavigationManager? Navigation { get; set; }

        [Parameter]
        public int GroupUserId { get; set; }

        private ApiResponse<GroupUserTestDTO>? UserTest;
        private List<ResultTestDTO>? TestQuestions;

        private Countdown Timer = default!;
        private int SecondsRemain = 0;
        private int CurrentQuestionIndex = 0;
        private int TestProgressPercent => (int)((double)(TestQuestions!.Count(t => t.AnswerId != null)) / TestQuestions!.Count * 100);

        protected override async Task OnInitializedAsync()
        {
            UserTest = await ServiceAPI!.GroupUserService.GetTestGroupUser(GroupUserId, "Group.Setting,Results.Question.Answers");
            TestQuestions = [];

            if (UserTest.Data!.Results != null && UserTest.Data.Results.Count == 0)
            {
                var questionResponse = await ServiceAPI!.TopicQuestionService.GetAllQuestionsBySpecializationId(UserTest.Data.SpecializationId, "Topics.TopicQuestion.Question.Answers");
                if (!questionResponse.Success)
                    await ErrorLoadingData<List<QuestionTestDTO>>(questionResponse);
                else
                {
                    Shuffle(questionResponse.Data!);

                    foreach (var question in questionResponse.Data!.GetRange(0, UserTest.Data.CountOfQuestion))
                    {
                        Shuffle(question.AnswerItems!);
                        var result = new ResultTestDTO
                        {
                            GroupUserId = GroupUserId,
                            Question = question,
                            AnswerId = null
                        };
                        TestQuestions.Add(result);
                    }
                    var resultResponse = await ServiceAPI!.ResultService.AddRangeTestResultAsync(TestQuestions, "Question.Answers");
                    if (!resultResponse.Success)
                        await ErrorLoadingData<List<ResultTestDTO>>(resultResponse);
                    else
                    {
                        TestQuestions = resultResponse.Data;

                        SecondsRemain = (int)UserTest.Data!.Timer.TotalSeconds;
                        UserTest.Data.EndTest = DateTime.Now + UserTest.Data.Timer;
                        await ServiceAPI!.GroupUserService.UpdateGroupUser(UserTest.Data.Id, new() { EndTest = UserTest.Data.EndTest });
                        await StartTest();
                    }
                }
            }
            else
            {
                TimeSpan interval = UserTest.Data.EndTest!.Value - DateTime.Now;
                SecondsRemain = (int)interval.TotalSeconds;
                foreach (var result in UserTest.Data.Results!)
                    TestQuestions.Add(result);
                await StartTest();
            }
        }

        private async Task ErrorLoadingData<T>(ApiResponse<T> response) where T : class
        {
            var settings = new NexusDialogSetting("Ошибка!", $"{response.Error}", "Вернуться на главную страницу");
            await DialogService!.Show(settings);
            Navigation!.NavigateTo("/user");
        }

        private async Task StartTest()
        {
            await InvokeAsync(StateHasChanged);
            Timer.Start(SecondsRemain);
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        private void TimerPauseCallback(int seconds) => SecondsRemain = seconds;

        private async Task TimerOutCallback()
        {
            var settings = new NexusDialogSetting("Время истекло!", "Время на прохождение теста закончилось! Тест завершен", "Завершить");
            await DialogService!.Show(settings);
            await SaveResults();
        }

        private async Task SetAnswer(ResultTestDTO result, int? value)
        {
            if (result.AnswerId != value)
            {
                result.AnswerId = value;
                await ServiceAPI!.ResultService.UpdateTestResult(result);
            }
        }

        private void PrevQuestion()
        {
            if (CurrentQuestionIndex > 0)
                CurrentQuestionIndex--;
        }

        private void NextQuestion()
        {
            if (CurrentQuestionIndex < TestQuestions!.Count - 1)
                CurrentQuestionIndex++;
        }

        private void GoToQuestion(int index) => CurrentQuestionIndex = index;

        private async Task CompleteTest()
        {
            if (TestProgressPercent != 100)
            {
                var settings = new NexusDialogSetting("Внимание!", "Не на все вопросы даны ответы. Вы уверены, что хотите завершить тест?", "Отменить", "Завершить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                    await SaveResults();
            }
            else if (SecondsRemain != 0)
            {
                var settings = new NexusDialogSetting("Внимание!", "Время на прохождение теста еще не истекло. Вы уверены, что хотите завершить тест?", "Отменить", "Завершить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                    await SaveResults();
            }
        }

        private async Task SaveResults()
        {
            UserTest!.Data!.Status = 3;
            await ServiceAPI!.GroupUserService.UpdateGroupUser(UserTest.Data.Id, new() { Status = 3 });
            Navigation!.NavigateTo("/user");
        }
    }
}
