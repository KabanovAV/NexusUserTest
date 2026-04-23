using NexusUserTest.Domain.Common;

namespace NexusUserTest.Infrastructure
{
    public class Repository(ApplicationDbContext db) : IRepository
    {
        public IAnswerRepository Answer { get; private set; } = new AnswerRepository(db);
        public IGroupRepository Group { get; private set; } = new GroupRepository(db);
        public IGroupUserRepository GroupUser { get; private set; } = new GroupUserRepository(db);
        public ITopicQuestionRepository TopicQuestion { get; private set; } = new TopicQuestionRepository(db);
        public IQuestionRepository Question { get; private set; } = new QuestionRepository(db);
        public IResultRepository Result { get; private set; } = new ResultRepository(db);
        public ISettingRepository Setting { get; private set; } = new SettingRepository(db);
        public ISpecializationRepository Specialization { get; private set; } = new SpecializationRepository(db);
        public ITopicRepository Topic { get; private set; } = new TopicRepository(db);
        public IUserRepository User { get; private set; } = new UserRepository(db);
    }
}
