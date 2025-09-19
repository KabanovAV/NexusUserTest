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

        //IResultService ResultService { get; }
        //ITopicQuestionService TopicQuestionService { get; }
    }

    public class APIService : IAPIService
    {
        private readonly Lazy<ISpecializationAPIService> _specializationService;
        private readonly Lazy<IGroupAPIService> _groupService;
        private readonly Lazy<IUserAPIService> _userService;
        private readonly Lazy<ITopicAPIService> _topicService;
        private readonly Lazy<IQuestionAPIService> _questionService;
        private readonly Lazy<IAnswerAPIService> _answerService;
        private readonly Lazy<IGroupUserAPIService> _groupUserService;
        private readonly Lazy<ISettingAPIService> _settingService;

        //private readonly Lazy<IResultService> _resultService;
        //private readonly Lazy<ITopicQuestionService> _topicQuestionService;

        public APIService(IHttpClientFactory httpClienFactory)
        {
            _specializationService = new Lazy<ISpecializationAPIService>(() => new SpecializationAPIService(httpClienFactory));
            _groupService = new Lazy<IGroupAPIService>(() => new GroupAPIService(httpClienFactory));
            _userService = new Lazy<IUserAPIService>(() => new UserAPIService(httpClienFactory));
            _topicService = new Lazy<ITopicAPIService>(() => new TopicAPIService(httpClienFactory));
            _questionService = new Lazy<IQuestionAPIService>(() => new QuestionAPIService(httpClienFactory));
            _answerService = new Lazy<IAnswerAPIService>(() => new AnswerAPIService(httpClienFactory));
            _groupUserService = new Lazy<IGroupUserAPIService>(() => new GroupUserAPIService(httpClienFactory));
            _settingService = new Lazy<ISettingAPIService>(() => new SettingAPIService(httpClienFactory));

            //_resultService = new Lazy<IResultService>(() => new ResultService(repository));
            //_topicQuestionService = new Lazy<ITopicQuestionService>(() => new TopicQuestionService(repository));
        }

        public ISpecializationAPIService SpecializationService => _specializationService.Value;
        public IGroupAPIService GroupService => _groupService.Value;
        public IUserAPIService UserService => _userService.Value;
        public ITopicAPIService TopicService => _topicService.Value;
        public IQuestionAPIService QuestionService => _questionService.Value;
        public IAnswerAPIService AnswerService => _answerService.Value;
        public IGroupUserAPIService GroupUserService => _groupUserService.Value;
        public ISettingAPIService SettingService => _settingService.Value;

        //public IResultService ResultService => _resultService.Value;
        //public ITopicQuestionService TopicQuestionService => _topicQuestionService.Value;
    }
}
