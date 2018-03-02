using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class CategoryTest
    {
        Category t1, t2, t3, t4, t5;
        public CategoryTest()
        {
            t1 = new Category { Id = 1, Name = "food" };
            t2 = new Category { Id = 2, Name = "meat" };
            t3 = new Category { Id = 3, Name = "drink" };
            t4 = new Category { Id = 4, Name = "meal" };
            t5 = new Category { Id = 5, Name = "dog" };
        }
        [Fact]
        public void TestCategoriesAdd()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Category test;
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

                    context.Categories.Add(t1);
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
                    test = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food", test.Name);
            }
            finally
            {
                connection.Close();
            }

        }
        [Fact]
        public void TestCategoriesUpdate()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Category test;
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

                    context.Categories.Add(t1);
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
                    var testUpdate = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Categories.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);                
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public void TestCategoriesDelete()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Category test;
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

                    context.Categories.Add(t1);
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
                    var testDelete = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                    context.Categories.Attach(testDelete);
                    context.Categories.Remove(testDelete);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                }
                Assert.Null(test);                
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public void TestCategoryNeighbors()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Category test;
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
                        new CategoryNeighbor { Left = t1, Right = t2 },
                        new CategoryNeighbor { Left = t1, Right = t3 },
                        new CategoryNeighbor { Left = t1, Right = t4 },
                        new CategoryNeighbor { Left = t2, Right = t4 }
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
                    var testUpdate = context.Categories.Where(t => t.Id == 1).FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Categories.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Categories.Where(t => t.Id == 1)
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
