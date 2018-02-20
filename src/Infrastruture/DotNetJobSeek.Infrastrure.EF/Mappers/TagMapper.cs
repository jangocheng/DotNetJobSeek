using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class TagMapper : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasIndex(t => t.Name)
            .IsUnique();
            
            builder.HasIndex(t => t.Version);
            builder.Property(t => t.Version).HasDefaultValue(0);
        }
    }
}