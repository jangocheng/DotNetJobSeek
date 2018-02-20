using System.Collections.Generic;

namespace DotNetJobSeek.Domain
{
    public class TagKeyword
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }

        public int Weight { get; set; }
    }
}