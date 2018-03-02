using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class SkillNeighborMapper : IEntityTypeConfiguration<SkillNeighbor>
    {
        public void Configure(EntityTypeBuilder<SkillNeighbor> builder)
        {
            builder.HasKey(t => new { t.LeftId, t.RightId });
            // many to many
            builder.HasOne(tk => tk.Left).WithMany(t => t.Lefts).HasForeignKey(tk => tk.LeftId);
            builder.HasOne(tk => tk.Right).WithMany(k => k.Rights).HasForeignKey(tk => tk.RightId);

            builder.HasIndex(t => t.Weight);
            builder.Property(t => t.Weight).HasDefaultValue(99);
        }
    }
}