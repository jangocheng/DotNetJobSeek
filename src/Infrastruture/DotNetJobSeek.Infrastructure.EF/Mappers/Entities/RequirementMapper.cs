using System;
using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class RequirementMapper : EntityMapper<Requirement>
    {
        public new void ConfigureOther(EntityTypeBuilder<Requirement> builder)
        {
            builder.HasOne(l => l.Job).WithMany(lt => lt.Requirements).HasForeignKey(l => l.JobId);
        }
    }
}