namespace DotNetJobSeek.Domain
{
    public class TagNeighbor
    {
       public int LeftId { get; set; }
       public Tag Left { get; set; }

       public int RightId { get; set; }
       public Tag Right { get; set; }

       public int Weight { get; set; }
    }
}