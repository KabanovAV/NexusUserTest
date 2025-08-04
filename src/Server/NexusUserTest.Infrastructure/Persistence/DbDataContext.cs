using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class DbDataContext : DbContext
    {
        public DbSet<Specialization> Specializations { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<GroupUser> GroupUsers { get; set; } = null!;
        public DbSet<TopicQuestion> TopicQuestions { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;

        public DbDataContext(DbContextOptions<DbDataContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        }
    }
}
