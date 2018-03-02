using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Skill
    {
        public int Id { set; get; }
        public string Name { set; get; } 

        public virtual ICollection<SkillTag> SkillTags { get; set; }
        
        public virtual ICollection<SkillNeighbor> Lefts { get; set; }
        public virtual ICollection<SkillNeighbor> Rights { get; set; }

    }
}