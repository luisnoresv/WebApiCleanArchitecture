using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.DisplayName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.PhotoUrl)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Content)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(20);
        }
    }
}