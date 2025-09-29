using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;

namespace NexusUserTest.Infrastructure
{
    public class TopicQuestionRepository(DbDataContext db) : RepositoryBase<TopicQuestion>(db), ITopicQuestionRepository
    {
        public async Task AddTopicQuestionAsync(TopicQuestion entity) => await AddAsync(entity);
        public void UpdateTopicQuestion(TopicQuestion entity) => Update(entity);
        public void DeleteTopicQuestion(TopicQuestion entity) => Delete(entity);
    }
}
