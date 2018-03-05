using System;
using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public abstract class EntityMapper<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasIndex(t => t.Created);
            builder.Property(t => t.Created).HasDefaultValue(DateTime.Now);

            builder.HasIndex(t => t.Updated);
            builder.Property(t => t.Updated).HasDefaultValue(DateTime.Now);

            this.ConfigureOther(builder);
        }
        public virtual void ConfigureOther(EntityTypeBuilder<T> builder)
        {

        }
    }
}