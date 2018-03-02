using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Requirement : Entity
    {
        public string Description { get; set; }
    }
}