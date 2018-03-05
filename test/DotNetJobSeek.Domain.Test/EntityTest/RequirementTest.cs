using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class RequirementTest
    {
        Requirement s1, s2, s3, s4, s5;
        public RequirementTest()
        {
            s1 = new Requirement { Description = "food" };
            s2 = new Requirement { Description = "meat" };
            s3 = new Requirement { Description = "drink" };
            s4 = new Requirement { Description = "meal" };
            s5 = new Requirement { Description = "dog" };
        }
        [Fact]
        public void TestInsert()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Requirement test;
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
                    context.Requirements.AddRange(s1, s2, s3,s4);
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
                    test = context.Requirements.FirstOrDefault();
                }
                Assert.Equal("food", test.Description);
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

                    context.Requirements.AddRange(s1, s2, s3,s4);
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
                    var testDelete = context.Requirements.FirstOrDefault();

                    context.Requirements.Attach(testDelete);
                    context.Requirements.Remove(testDelete);
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
                    int count = context.Requirements.Select(t => t.Id).Count();
                    Assert.Equal(3, count);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
