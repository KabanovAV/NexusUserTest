using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Lastname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Login)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.HasMany(s => s.GroupUsers)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
