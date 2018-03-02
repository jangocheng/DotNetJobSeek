using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class ValueObjectMapper<T> : IEntityTypeConfiguration<T> where T : ValueObject
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasIndex(k => k.Name)
            .IsUnique();
        }
    }
}