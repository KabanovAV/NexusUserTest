namespace NexusUserTest.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IAnswerRepository Answer { get; }
        IGroupRepository Group { get; }
        IGroupUserRepository GroupUser { get; }
        ITopicQuestionRepository TopicQuestion { get; }
        IQuestionRepository Question { get; }
        IResultRepository Result { get; }
        ISettingRepository Setting { get; }
        ISpecializationRepository Specialization { get; }
        ITopicRepository Topic { get; }
        IUserRepository User { get; }

        void Save();
        Task SaveAsync();
    }
}
