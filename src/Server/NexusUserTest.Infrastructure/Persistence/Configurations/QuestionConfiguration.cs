using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(a => a.Answers)
                .WithOne(q => q.Question)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
