namespace DotNetJobSeek.Domain
{
    public class KeywordNeighbor
    {
       public int LeftId { get; set; }
       public Keyword Left { get; set; }

       public int RightId { get; set; }
       public Keyword Right { get; set; }

       public int Weight { get; set; }
    }
}