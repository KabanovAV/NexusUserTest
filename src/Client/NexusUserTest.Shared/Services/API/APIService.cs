namespace NexusUserTest.Shared.Services
{
    public interface IAPIService
    {
        ISpecializationAPIService SpecializationService { get; }
        IGroupAPIService GroupService { get; }
        ITopicAPIService TopicService { get; }
        IUserAPIService UserService { get; }
        IQuestionAPIService QuestionService { get; }
        IAnswerAPIService AnswerService { get; }
        IGroupUserAPIService GroupUserService { get; }
        ISettingAPIService SettingService { get; }
        ITopicQuestionAPIService TopicQuestionService { get; }
        IResultAPIService ResultService { get; }
    }

    public class APIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IAPIService
    {
        private readonly Lazy<ISpecializationAPIService> _specializationService = new (() => new SpecializationAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IGroupAPIService> _groupService = new (() => new GroupAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IUserAPIService> _userService = new (() => new UserAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<ITopicAPIService> _topicService = new (() => new TopicAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IQuestionAPIService> _questionService = new (() => new QuestionAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IAnswerAPIService> _answerService = new (() => new AnswerAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IGroupUserAPIService> _groupUserService = new (() => new GroupUserAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<ISettingAPIService> _settingService = new (() => new SettingAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<ITopicQuestionAPIService> _topicQuestionService = new (() => new TopicQuestionAPIService(httpClienFactory, responseHandler));
        private readonly Lazy<IResultAPIService> _resultService = new(() => new ResultAPIService(httpClienFactory, responseHandler));

        public ISpecializationAPIService SpecializationService => _specializationService.Value;
        public IGroupAPIService GroupService => _groupService.Value;
        public IUserAPIService UserService => _userService.Value;
        public ITopicAPIService TopicService => _topicService.Value;
        public IQuestionAPIService QuestionService => _questionService.Value;
        public IAnswerAPIService AnswerService => _answerService.Value;
        public IGroupUserAPIService GroupUserService => _groupUserService.Value;
        public ISettingAPIService SettingService => _settingService.Value;
        public ITopicQuestionAPIService TopicQuestionService => _topicQuestionService.Value;
        public IResultAPIService ResultService => _resultService.Value;
    }
}
