using System.Collections.Generic;

namespace DotNetJobSeek.Domain
{
    public class JobTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public string JobId { get; set; }
        public Job Job { get; set; }

        public int Weight { get; set; }
    }
}