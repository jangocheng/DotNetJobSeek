using DotNetJobSeek.Domain.ValueObjects;
using System.Collections.Generic;

namespace DotNetJobSeek.Domain.ValueObjects
{
    public class PostKeyword
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }

        public int Weight { get; set; }
    }
}