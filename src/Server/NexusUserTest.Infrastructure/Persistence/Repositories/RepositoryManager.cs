using NexusUserTest.Domain.Repositories;

namespace NexusUserTest.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DbDataContext _db;
        public IAnswerRepository Answer { get; private set; }
        public IGroupRepository Group { get; private set; }
        public IGroupUserRepository GroupUser { get; private set; }
        public ITopicQuestionRepository TopicQuestion { get; private set; }
        public IQuestionRepository Question { get; private set; }
        public IResultRepository Result { get; private set; }
        public ISettingRepository Setting { get; private set; }
        public ISpecializationRepository Specialization { get; private set; }
        public ITopicRepository Topic { get; private set; }
        public IUserRepository User { get; private set; }

        public RepositoryManager(DbDataContext db)
        {
            _db = db;
            Answer = new AnswerRepository(db);
            Group = new GroupRepository(db);
            GroupUser = new GroupUserRepository(db);
            TopicQuestion = new TopicQuestionRepository(db);
            Question = new QuestionRepository(db);
            Result = new ResultRepository(db);
            Setting = new SettingRepository(db);
            Specialization = new SpecializationRepository(db);
            Topic = new TopicRepository(db);
            User = new UserRepository(db);
        }

        public void Save() => _db.SaveChanges();
    }
}
