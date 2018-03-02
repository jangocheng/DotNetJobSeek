using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Skill : ValueObject
    {

        public virtual ICollection<SkillTag> SkillTags { get; set; }

        public virtual ICollection<SkillNeighbor> Lefts { get; set; }
        public virtual ICollection<SkillNeighbor> Rights { get; set; }

    }
}