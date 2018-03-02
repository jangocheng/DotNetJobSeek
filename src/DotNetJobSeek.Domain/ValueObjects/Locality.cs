using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Locality
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public string Postcode { get; set; }

       public int StateId { get; set; }
       public State State { get; set; }

       public ICollection<Location> Locations { get; set; }
    }
}