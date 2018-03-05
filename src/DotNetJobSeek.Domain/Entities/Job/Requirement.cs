using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Requirement : Entity
    {
        public string Description { get; set; }
        public string JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}