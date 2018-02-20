using DotNetJobSeek.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotNetJobSeek.Domain
{
    public class Tag
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int Version { get; set; }

       public virtual ICollection<TagKeyword> TagKeywords { get; } = new List<TagKeyword>();

    //    [NotMapped]
    //    public IEnumerable<Keyword> Keywords => TagKeywords.Select(e => new Keyword {Id = e.Keyword.Id, Name = e.Keyword.Name, Weight = e.Weight });
    }
}