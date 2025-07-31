using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure.Persistence
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasMany(s => s.Groups)
                .WithOne(g => g.Specialization)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Topics)
                .WithOne(t => t.Specialization)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
