using NexusUserTest.Domain.Repositories;
using SibCCSPETest.ServiceBase;

namespace NexusUserTest.Application.Services
{
    public interface IRepoServiceManager
    {
        ISpecializationRepoService SpecializationRepository { get; }
        IAnswerRepoService AnswerRepository { get; }
        IGroupRepoService GroupRepository { get; }
        IQuestionRepoService QuestionRepository { get; }
        IResultRepoService ResultRepository { get; }
        ISettingRepoService SettingRepository { get; }
        IGroupUserRepoService GroupUserRepository { get; }
        ITopicQuestionRepoService TopicQuestionRepository { get; }
        ITopicRepoService TopicRepository { get; }
        IUserRepoService UserRepository { get; }
    }

    public class RepoServiceManager : IRepoServiceManager
    {
        private readonly Lazy<ISpecializationRepoService> _specialization;
        private readonly Lazy<IAnswerRepoService> _answer;
        private readonly Lazy<IGroupRepoService> _group;
        private readonly Lazy<IQuestionRepoService> _question;
        private readonly Lazy<IResultRepoService> _result;
        private readonly Lazy<ISettingRepoService> _setting;
        private readonly Lazy<IGroupUserRepoService> _groupUser;
        private readonly Lazy<ITopicQuestionRepoService> _topicQuestion;
        private readonly Lazy<ITopicRepoService> _topic;
        private readonly Lazy<IUserRepoService> _user;

        public RepoServiceManager(IRepositoryManager repository)
        {
            _specialization = new Lazy<ISpecializationRepoService>(() => new SpecializationRepoService(repository));
            _answer = new Lazy<IAnswerRepoService>(() => new AnswerRepoService(repository));
            _group = new Lazy<IGroupRepoService>(() => new GroupRepoService(repository));
            _question = new Lazy<IQuestionRepoService>(() => new QuestionRepoService(repository));
            _result = new Lazy<IResultRepoService>(() => new ResultRepoService(repository));
            _setting = new Lazy<ISettingRepoService>(() => new SettingRepoService(repository));
            _groupUser = new Lazy<IGroupUserRepoService>(() => new GroupUserRepoService(repository));
            _topicQuestion = new Lazy<ITopicQuestionRepoService>(() => new TopicQuestionRepoService(repository));
            _topic = new Lazy<ITopicRepoService>(() => new TopicRepoService(repository));
            _user = new Lazy<IUserRepoService>(() => new UserRepoService(repository));
        }

        public ISpecializationRepoService SpecializationRepository => _specialization.Value;
        public IAnswerRepoService AnswerRepository => _answer.Value;
        public IGroupRepoService GroupRepository => _group.Value;
        public IQuestionRepoService QuestionRepository => _question.Value;
        public IResultRepoService ResultRepository => _result.Value;
        public ISettingRepoService SettingRepository => _setting.Value;
        public IGroupUserRepoService GroupUserRepository => _groupUser.Value;
        public ITopicQuestionRepoService TopicQuestionRepository => _topicQuestion.Value;
        public ITopicRepoService TopicRepository => _topic.Value;
        public IUserRepoService UserRepository => _user.Value;
    }
}
