using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class LocalityMapper : IEntityTypeConfiguration<Locality>
    {
        public void Configure(EntityTypeBuilder<Locality> builder)
        {
            builder.HasIndex(k => k.Postcode);
            builder.HasOne(l => l.State).WithMany(lt => lt.Localities).HasForeignKey(l => l.StateId);
        }
    }
}