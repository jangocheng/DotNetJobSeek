using System;
using Xunit;
using DotNetJobSeek.Domain.ValueObjects;

namespace DotNetJobSeek.Domain.Test
{
    public class ValueObjectsTest
    {
        Keyword k1, k2;
        Tag t1, t2;
        public ValueObjectsTest()
        {
            k1 = new Keyword { Id = 1, Name = "food" };
            k2 = new Keyword { Id = 2, Name = "place" };
            t1 = new Tag { Id = 1, Name = "good" };
            t2 = new Tag { Id = 2, Name = "china" };
        }
        [Fact]
        public void TestKeywords()
        {
            Assert.Equal(1, k1.Id);
        }
    }
}
