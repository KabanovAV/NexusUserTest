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

        //IResultService ResultService { get; }
        //ISettingService SettingService { get; }
        //IGroupUserService GroupUserService { get; }
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

        //private readonly Lazy<IResultService> _resultService;
        //private readonly Lazy<ISettingService> _settingService;
        //private readonly Lazy<IGroupUserService> _groupUserService;
        //private readonly Lazy<ITopicQuestionService> _topicQuestionService;

        public APIService(IHttpClientFactory httpClienFactory)
        {
            _specializationService = new Lazy<ISpecializationAPIService>(() => new SpecializationAPIService(httpClienFactory));
            _groupService = new Lazy<IGroupAPIService>(() => new GroupAPIService(httpClienFactory));
            _userService = new Lazy<IUserAPIService>(() => new UserAPIService(httpClienFactory));
            _topicService = new Lazy<ITopicAPIService>(() => new TopicAPIService(httpClienFactory));
            _questionService = new Lazy<IQuestionAPIService>(() => new QuestionAPIService(httpClienFactory));
            _answerService = new Lazy<IAnswerAPIService>(() => new AnswerAPIService(httpClienFactory));

            //_resultService = new Lazy<IResultService>(() => new ResultService(repository));
            //_settingService = new Lazy<ISettingService>(() => new SettingService(repository));
            //_groupUserService = new Lazy<IGroupUserService>(() => new GroupUserService(repository));
            //_topicQuestionService = new Lazy<ITopicQuestionService>(() => new TopicQuestionService(repository));
        }

        public ISpecializationAPIService SpecializationService => _specializationService.Value;
        public IGroupAPIService GroupService => _groupService.Value;
        public IUserAPIService UserService => _userService.Value;
        public ITopicAPIService TopicService => _topicService.Value;
        public IQuestionAPIService QuestionService => _questionService.Value;
        public IAnswerAPIService AnswerService => _answerService.Value;

        //public IResultService ResultService => _resultService.Value;
        //public ISettingService SettingService => _settingService.Value;
        //public IGroupUserService GroupUserService => _groupUserService.Value;
        //public ITopicQuestionService TopicQuestionService => _topicQuestionService.Value;


    }
}
