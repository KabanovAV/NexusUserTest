using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Groups)
                .WithOne(g => g.Specialization)
                .HasForeignKey(g => g.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Topics)
                .WithOne(t => t.Specialization)
                .HasForeignKey(t => t.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
