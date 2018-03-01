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
        Keyword k1;
        public KeywordTest()
        {
            k1 = new Keyword { Id = 1, Name = "food" };
        }
        [Fact]
        public void TestKeywordsAdd()
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
                    test = context.Keywords.Where(k => k.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food", test.Name);
            }
            finally
            {
                connection.Close();
            }

        }
        [Fact]
        public void TestKeywordsUpdate()
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
        public void TestKeywordNeighbors()
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
    }
}
