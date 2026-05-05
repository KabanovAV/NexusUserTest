using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(g => g.Begin)
                .IsRequired();

            builder.Property(g => g.End)
                .IsRequired();

            builder.HasMany(g => g.GroupUser)
                .WithOne(gu => gu.Group)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Setting)
                .WithOne(g => g.Group)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
