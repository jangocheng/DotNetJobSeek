using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Category
    {
       public int Id { set; get; }
       public string Name { set; get; } 

       public virtual ICollection<CategoryNeighbor> Lefts { get; set; }
       public virtual ICollection<CategoryNeighbor> Rights { get; set; }

    }
}