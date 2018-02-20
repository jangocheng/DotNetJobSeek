using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotNetJobSeek.Domain
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public int Weight { get; set; }


        public virtual ICollection<TagKeyword> TagKeywords { get; } = new List<TagKeyword>();

        // [NotMapped]
        // public IEnumerable<Tag> Tags => TagKeywords.Select(e => e.Tag);
    }
}