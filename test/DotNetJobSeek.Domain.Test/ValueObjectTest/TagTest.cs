using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class TagTest
    {
        Tag t1, t2, t3, t4, t5;
        public TagTest()
        {
            t1 = new Tag { Name = "food" };
            t2 = new Tag { Name = "meat" };
            t3 = new Tag { Name = "drink" };
            t4 = new Tag { Name = "meal" };
            t5 = new Tag { Name = "dog" };
        }
        [Fact]
        public void TestTagsAdd()
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
                    test = context.Tags.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food", test.Name);
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestTagsDelete()
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
                    var testDelete = context.Tags.Where(t => t.Name == "food").FirstOrDefault();

                    context.Tags.Attach(testDelete);
                    context.Tags.Remove(testDelete);
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
                    test = context.Tags.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Null(test);
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestTagsUpdate()
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
                    var testUpdate = context.Tags.Where(t => t.Id == 1).FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Tags.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Tags.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);                
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public void TestTagNeighbors()
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

                    context.AddRange(
                        new TagNeighbor { Left = t1, Right = t2 },
                        new TagNeighbor { Left = t1, Right = t3 },
                        new TagNeighbor { Left = t1, Right = t4 },
                        new TagNeighbor { Left = t2, Right = t4 }
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
                    var testUpdate = context.Tags.Where(t => t.Id == 1).FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Tags.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Tags.Where(t => t.Id == 1)
                            .Include(t => t.Rights)
                                .ThenInclude(tn => tn.Left)
                            .Include(t => t.Lefts)
                                .ThenInclude(tn => tn.Right)
                    .FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);  
                Assert.Equal(3, test.Lefts.Count);
                Assert.Equal("meat", test.Lefts.Where(r => r.RightId== 2).First().Right.Name);          
            }
            finally
            {
                connection.Close();
            }

        }        
    }
}
