using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class SkillTagMapper : IEntityTypeConfiguration<SkillTag>
    {
        public void Configure(EntityTypeBuilder<SkillTag> builder)
        {
            builder.HasKey(t => new { t.TagId, t.SkillId });
            // many to many
            builder.HasOne(tk => tk.Tag).WithMany(t => t.SkillTags).HasForeignKey(tk => tk.TagId);
            builder.HasOne(tk => tk.Skill).WithMany(k => k.SkillTags).HasForeignKey(tk => tk.SkillId);

            builder.HasIndex(t => t.Weight);
            builder.Property(t => t.Weight).HasDefaultValue(0);
        }
    }
}