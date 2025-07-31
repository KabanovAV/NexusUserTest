using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Repositories
{
    public interface ITopicQuestionRepository : IRepositoryBase<TopicQuestion>
    {
        Task AddTopicQuestionAsync(TopicQuestion entity);
        void UpdateTopicQuestion(TopicQuestion entity);
        void DeleteTopicQuestion(TopicQuestion entity);
    }
}
