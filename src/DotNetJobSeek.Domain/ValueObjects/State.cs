using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class State : ValueObject
    {
        public string Country { set; get; } 

        public virtual ICollection<Locality> Localities { set; get; }
    }
}