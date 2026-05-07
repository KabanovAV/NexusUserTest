using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class TopicQuestionRepository(ApplicationDbContext db) : RepositoryOperations<TopicQuestion>(db), ITopicQuestionRepository
    {

    }
}
