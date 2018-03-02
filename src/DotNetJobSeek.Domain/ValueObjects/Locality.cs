using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Locality : ValueObject
    {
       public string Postcode { get; set; }

       public int StateId { get; set; }
       public State State { get; set; }

       public virtual ICollection<Location> Locations { get; set; }
    }
}