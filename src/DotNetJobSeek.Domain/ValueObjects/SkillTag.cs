using System.Collections.Generic;

namespace DotNetJobSeek.Domain
{
    public class SkillTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public int Weight { get; set; }
    }
}