using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class SkillMapper : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasIndex(k => k.Name)
            .IsUnique();
        }
    }
}