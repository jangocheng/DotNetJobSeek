using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class State
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Country { set; get; } 

        public ICollection<Locality> Localities { set; get; }
    }
}