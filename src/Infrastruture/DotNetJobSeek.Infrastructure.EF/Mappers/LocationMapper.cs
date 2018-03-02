using DotNetJobSeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class LocationMapper : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(k => k.Geohash);
            builder.HasOne(l => l.Locality).WithMany(lt => lt.Locations).HasForeignKey(l => l.LocalityId);
        }
    }
}