using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Location
    {
        public string Geohash { get; set; }
        public string Address { get; set; }

        public int? LocalityId { get; set; }
        public virtual Locality Locality { get; set; }
    }
}