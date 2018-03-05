using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Spatial;

namespace DotNetJobSeek.Domain
{
    public class Location
    {
        public string Geohash { get; set; }
        public string Address { get; set; }
        // public string LatLng { get; set; }
        // public DbGeography Location { get; set; }

        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}