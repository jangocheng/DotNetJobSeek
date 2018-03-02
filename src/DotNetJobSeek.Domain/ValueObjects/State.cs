using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class State
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Country { set; get; } 

        public virtual ICollection<Locality> Localities { set; get; }
    }
}