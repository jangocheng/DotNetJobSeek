using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class KeywordTest
    {
        Keyword k1, k2, k3, k4, k5;
        public KeywordTest()
        {
            k1 = new Keyword { Name = "food" };
            k2 = new Keyword { Name = "meat" };
            k3 = new Keyword { Name = "drink" };
            k4 = new Keyword { Name = "meal" };
            k5 = new Keyword { Name = "dog" };
        }
        [Fact]
        public void TestCreate()
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

                    context.Keywords.Add(k1);
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
                    test = context.Keywords.Where(k => k.Name == "food").FirstOrDefault();
                }
                Assert.Equal("food", test.Name);
            }
            finally
            {
                connection.Close();
            }

        }
        [Fact]
        public void TestUpdate()
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

                    context.Keywords.Add(k1);
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

        [Fact]
        public void TestDelete()
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

                    context.Keywords.Add(k1);
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
                    var testDelete = new Keyword { Id = 1 };
                    context.Keywords.Attach(testDelete);
                    context.Keywords.Remove(testDelete);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Keywords.Where(k => k.Name == "food").FirstOrDefault();
                }
                Assert.Null(test);                
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestNeighbors()
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
                        new KeywordNeighbor { Left = k1, Right = k2 },
                        new KeywordNeighbor { Left = k1, Right = k3 },
                        new KeywordNeighbor { Left = k1, Right = k4 },
                        new KeywordNeighbor { Left = k2, Right = k4 }
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
                            .Include(k => k.Rights)
                                .ThenInclude(tn => tn.Left)
                            .Include(k => k.Lefts)
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
