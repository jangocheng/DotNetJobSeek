using DotNetJobSeek.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotNetJobSeek.Domain
{
    public class Tag
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int Version { get; set; }

       public virtual ICollection<TagKeyword> TagKeywords { get; set; }
       
       public virtual ICollection<TagNeighbor> Lefts { get; set; }
       public virtual ICollection<TagNeighbor> Rights { get; set; }

    }
}