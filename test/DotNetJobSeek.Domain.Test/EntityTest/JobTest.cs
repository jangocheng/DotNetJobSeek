using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetJobSeek.Domain.Test
{
    public class JobTest
    {
        Requirement s1, s2, s3, s4, s5;
        Job j1;
        Location l1;
        Category c1;
        Tag t1, t2;
        public JobTest()
        {
            j1 = new Job { Name = "Full Stack Developer", Type = "1" , Description = "What is what", Salary = " from 11 to 22"};
            
            s1 = new Requirement { Description = "food" };
            s2 = new Requirement { Description = "meat" };
            s3 = new Requirement { Description = "drink" };
            s4 = new Requirement { Description = "meal" };
            s5 = new Requirement { Description = "dog" };
            l1 = new Location { Geohash = "asdf", Address = "Hobart" };
            c1 = new Category { Name = "programmer" };
            t1 = new Tag { Name = "pr" };
            t2 = new Tag { Name = "Cc" };
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
                    j1.Requirements = new[] {s1,s2,s3};
                    j1.Category = c1;
                    j1.Location = l1;
                    j1.JobTags = new[] 
                    {
                        new JobTag { Job = j1, Tag = t1 },
                        new JobTag { Job = j1, Tag = t2 }
                    };
                    context.Jobs.Add(j1);
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
                    c1 = context.Categories.FirstOrDefault();
                    l1 = context.Locations.FirstOrDefault();
                    t1 = context.Tags.FirstOrDefault();
                }
                Assert.Equal("food", test.Description);
                Assert.Equal("programmer", c1.Name);
                Assert.Equal("Hobart", l1.Address);
                Assert.Equal("pr", t1.Name);
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

                    j1.Requirements = new[] {s1,s2,s3, s4};
                    j1.Category = c1;
                    j1.Location = l1;
                    j1.JobTags = new[] 
                    {
                        new JobTag { Job = j1, Tag = t1 },
                        new JobTag { Job = j1, Tag = t2 }
                    };
                    context.Jobs.Add(j1);
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
                    int count = context.Jobs
                        .Include(j => j.Requirements)
                    .FirstOrDefault().Requirements.Count();
                    Assert.Equal(3, count);
                }
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

                    j1.Requirements = new[] {s1, s2, s3, s4};
                    j1.Category = c1;
                    j1.Location = l1;
                    j1.JobTags = new[] 
                    {
                        new JobTag { Job = j1, Tag = t1 },
                        new JobTag { Job = j1, Tag = t2 }
                    };
                    context.Jobs.Add(j1);
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
                    var updateR1 = context.Requirements.FirstOrDefault();
                    updateR1.Description = "food1";
                    context.Requirements.Update(updateR1);

                    t1 = context.Tags.FirstOrDefault();
                    t1.Name = "Good";

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
                    s1 = context.Requirements.FirstOrDefault();
                    c1 = context.Categories.FirstOrDefault();
                    l1 = context.Locations.FirstOrDefault();
                    t1 = context.Tags.FirstOrDefault();
                }
                Assert.Equal("food1", s1.Description);
                Assert.Equal("programmer", c1.Name);
                Assert.Equal("Hobart", l1.Address);
                Assert.Equal("Good", t1.Name);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
