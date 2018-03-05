using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Category : ValueObject
    {
       public virtual ICollection<CategoryNeighbor> Lefts { get; set; }
       public virtual ICollection<CategoryNeighbor> Rights { get; set; }
       public virtual ICollection<Job> Jobs { get; set; }
    }
}