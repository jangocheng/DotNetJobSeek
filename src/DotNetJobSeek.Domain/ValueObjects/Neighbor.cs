using System.Collections.Generic;

namespace DotNetJobSeek.Domain
{
    public abstract class Neighbor<T> where T : ValueObject
    {
       public int LeftId { get; set; }
       public T Left { get; set; }

       public int RightId { get; set; }
       public T Right { get; set; }

       public int Weight { get; set; }
    }
}