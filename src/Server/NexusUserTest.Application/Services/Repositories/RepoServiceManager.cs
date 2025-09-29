using NexusUserTest.Domain.Repositories;

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

    public class RepoServiceManager(IRepositoryManager repository) : IRepoServiceManager
    {
        private readonly Lazy<ISpecializationRepoService> _specialization = new(() => new SpecializationRepoService(repository));
        private readonly Lazy<IAnswerRepoService> _answer = new(() => new AnswerRepoService(repository));
        private readonly Lazy<IGroupRepoService> _group = new(() => new GroupRepoService(repository));
        private readonly Lazy<IQuestionRepoService> _question = new(() => new QuestionRepoService(repository));
        private readonly Lazy<IResultRepoService> _result = new(() => new ResultRepoService(repository));
        private readonly Lazy<ISettingRepoService> _setting = new(() => new SettingRepoService(repository));
        private readonly Lazy<IGroupUserRepoService> _groupUser = new(() => new GroupUserRepoService(repository));
        private readonly Lazy<ITopicQuestionRepoService> _topicQuestion = new(() => new TopicQuestionRepoService(repository));
        private readonly Lazy<ITopicRepoService> _topic = new(() => new TopicRepoService(repository));
        private readonly Lazy<IUserRepoService> _user = new(() => new UserRepoService(repository));

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
