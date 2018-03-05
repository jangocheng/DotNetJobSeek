using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotNetJobSeek.Domain
{
    public class Keyword : ValueObject
    {
        public virtual ICollection<TagKeyword> TagKeywords { get; set; } 

        public virtual ICollection<KeywordNeighbor> Lefts { get; set; }
        public virtual ICollection<KeywordNeighbor> Rights { get; set; }

        // [NotMapped]
        // public IEnumerable<Tag> Tags => TagKeywords.Select(e => e.Tag);
    }
}