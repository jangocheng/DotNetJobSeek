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
            k1 = new Keyword { Id = 1, Name = "food" };
            k2 = new Keyword { Id = 2, Name = "drink" };
            k3 = new Keyword { Id = 3, Name = "hotel" };
            t1 = new Tag { Id = 1, Name = "bar"};
            t2 = new Tag { Id = 2, Name = "move"};
            t3 = new Tag { Id = 3, Name = "live"};
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
                    test = context.Keywords.Where(k => k.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);                
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
