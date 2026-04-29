using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Specialization> Specializations { get; }
        DbSet<Group> Groups { get; }
        DbSet<User> Users { get; }
        DbSet<Topic> Topics { get; }
        DbSet<Question> Questions { get; }
        DbSet<Answer> Answers { get; }
        DbSet<GroupUser> GroupUsers { get; }
        DbSet<TopicQuestion> TopicQuestions { get; }
        DbSet<TestResult> TestResults { get; }
        DbSet<TestSetting> TestSettings { get; }
    }
}
