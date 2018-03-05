using System;
using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class JobMapper : EntityMapper<Job>
    {
        public new void ConfigureOther(EntityTypeBuilder<Job> builder)
        {
            builder.HasOne(j => j.Category).WithMany(c => c.Jobs).HasForeignKey(j => j.CategoryId);
            builder.HasOne(j => j.Location).WithMany(c => c.Jobs).HasForeignKey(j => j.LocationGeohash);
        }
    }
}