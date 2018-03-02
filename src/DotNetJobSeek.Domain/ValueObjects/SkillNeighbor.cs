namespace DotNetJobSeek.Domain
{
    public class SkillNeighbor
    {
       public int LeftId { get; set; }
       public Skill Left { get; set; }

       public int RightId { get; set; }
       public Skill Right { get; set; }

       public int Weight { get; set; }
    }
}