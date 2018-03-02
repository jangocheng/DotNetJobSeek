namespace DotNetJobSeek.Domain
{
    public class CategoryNeighbor
    {
       public int LeftId { get; set; }
       public Category Left { get; set; }

       public int RightId { get; set; }
       public Category Right { get; set; }

       public int Weight { get; set; }
    }
}