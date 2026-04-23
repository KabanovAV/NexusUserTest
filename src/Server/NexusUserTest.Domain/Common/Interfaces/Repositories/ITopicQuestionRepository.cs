using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    public interface ITopicQuestionRepository : IRepositoryBase<TopicQuestion>
    {
        Task AddTopicQuestionAsync(TopicQuestion entity);
        void UpdateTopicQuestion(TopicQuestion entity);
        void DeleteTopicQuestion(TopicQuestion entity);
    }
}
