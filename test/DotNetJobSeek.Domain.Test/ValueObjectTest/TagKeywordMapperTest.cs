using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class TagKeywordMapperTest
    {
        Keyword k1, k2, k3;
        Tag t1, t2, t3;
        public TagKeywordMapperTest()
        {
            k1 = new Keyword { Name = "food" };
            k2 = new Keyword { Name = "drink" };
            k3 = new Keyword { Name = "hotel" };
            t1 = new Tag { Name = "bar"};
            t2 = new Tag { Name = "move"};
            t3 = new Tag { Name = "live"};
        }
        [Fact]
        public void TestTagAdd()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Tag test;
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EFContext>()
                    .UseSqlite(connection)
                    .Options;
                using(var context = new EFContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using(var context = new EFContext(options))
                {

                    context.Tags.Add(t1);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
                using(var context = new EFContext(options))
                {
                    test = context.Tags.Where(k => k.Id == 1).FirstOrDefault();
                }
                Assert.Equal("bar", test.Name);
                Assert.Equal(0, test.Version);
            }
            finally
            {
                connection.Close();
            }

        }
        [Fact]
        public void TestTagKeywordsAddUpdate()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Keyword test;
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EFContext>()
                    .UseSqlite(connection)
                    .Options;
                using(var context = new EFContext(options))
                {
                    context.Database.EnsureCreated();
                }
                using(var context = new EFContext(options))
                {

                    context.AddRange(
                        new TagKeyword { Tag = t1, Keyword = k1 },
                        new TagKeyword { Tag = t2, Keyword = k1 }
                    );
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Exception)
                    { 
                        throw;
                    }
                }

                using(var context = new EFContext(options))
                {
                    var testUpdate = context.Keywords.Where(k => k.Id == 1).FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Keywords.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Keywords.Where(k => k.Id == 1)
                                    .Include(t => t.TagKeywords)
                                        .ThenInclude(tk => tk.Tag)
                                .FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);
                Assert.Equal(2, test.TagKeywords.Count);
                Assert.Equal("bar", test.TagKeywords.Where(tk => tk.TagId == 1).First().Tag.Name);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
