using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class JobTagMapper : IEntityTypeConfiguration<JobTag>
    {
        public void Configure(EntityTypeBuilder<JobTag> builder)
        {
            builder.HasKey(t => new { t.TagId, t.JobId });
            // many to many
            builder.HasOne(tk => tk.Tag).WithMany(t => t.JobTags).HasForeignKey(tk => tk.TagId);
            builder.HasOne(tk => tk.Job).WithMany(k => k.JobTags).HasForeignKey(tk => tk.JobId);

            builder.HasIndex(t => t.Weight);
            builder.Property(t => t.Weight).HasDefaultValue(0);
        }
    }
}