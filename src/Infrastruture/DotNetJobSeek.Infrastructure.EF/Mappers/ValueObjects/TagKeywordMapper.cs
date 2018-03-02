using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class TagKeywordMapper : IEntityTypeConfiguration<TagKeyword>
    {
        public void Configure(EntityTypeBuilder<TagKeyword> builder)
        {
            builder.HasKey(t => new { t.TagId, t.KeywordId });
            // many to many
            builder.HasOne(tk => tk.Tag).WithMany(t => t.TagKeywords).HasForeignKey(tk => tk.TagId);
            builder.HasOne(tk => tk.Keyword).WithMany(k => k.TagKeywords).HasForeignKey(tk => tk.KeywordId);

            builder.HasIndex(t => t.Weight);
            builder.Property(t => t.Weight).HasDefaultValue(0);
        }
    }
}