using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasOne(s => s.Setting)
                .WithOne(g => g.Group)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
