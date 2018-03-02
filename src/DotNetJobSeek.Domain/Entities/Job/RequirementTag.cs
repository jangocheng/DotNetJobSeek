using System.Collections.Generic;

namespace DotNetJobSeek.Domain
{
    public class RequirementTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int RequirementId { get; set; }
        public Requirement Requirement { get; set; }

        public int Weight { get; set; }
    }
}