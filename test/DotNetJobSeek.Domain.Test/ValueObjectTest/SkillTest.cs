using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class SkillTest
    {
        Tag t1, t2, t3, t4, t5;
        Skill s1, s2, s3, s4;
        public SkillTest()
        {
            t1 = new Tag { Name = "food" };
            t2 = new Tag { Name = "meat" };
            t3 = new Tag { Name = "drink" };
            t4 = new Tag { Name = "meal" };
            t5 = new Tag { Name = "dog" };
            s1 = new Skill { Name = "C" };
            s2 = new Skill { Name = "git" };
            s3 = new Skill { Name = "java" };
            s4 = new Skill { Name = "C#" };
        }
        [Fact]
        public void TestInsert()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            ValueObject test;
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
                    context.Skills.AddRange(s1, s2, s3,s4);
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
                    test = context.Skills.Where(s => s.Name == "git").FirstOrDefault();
                }
                Assert.Equal("git", test.Name);
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

                    context.Skills.AddRange(s1, s2, s3,s4);
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
                    var testDelete = context.Skills.Where(t => t.Name == "git").FirstOrDefault();

                    context.Skills.Attach(testDelete);
                    context.Skills.Remove(testDelete);
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
                    int count = context.Skills.Select(t => t.Id).Count();
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
            ValueObject test;
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

                    context.Skills.AddRange(s1, s2, s3,s4);
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
                    var testUpdate = context.Skills.Where(t => t.Name == "C").FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Skills.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Skills.Where(t => t.Name == "food1").FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);                
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
            Skill test;
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
                        new SkillNeighbor { Left = s1, Right = s2 },
                        new SkillNeighbor { Left = s1, Right = s3 },
                        new SkillNeighbor { Left = s1, Right = s4 },
                        new SkillNeighbor { Left = s2, Right = s4 }
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
                    var testUpdate = context.Skills.Where(t => t.Name == "C").FirstOrDefault();
                    testUpdate.Name = "food1";
                    context.Skills.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Skills.Where(t => t.Name == "food1")
                            .Include(t => t.Rights)
                                .ThenInclude(tn => tn.Left)
                            .Include(t => t.Lefts)
                                .ThenInclude(tn => tn.Right)
                    .FirstOrDefault();
                }
                Assert.Equal("food1", test.Name);  
                Assert.Equal(3, test.Lefts.Count);
                Assert.Equal("git", test.Lefts.Where(r => r.RightId== 2).First().Right.Name);          
            }
            finally
            {
                connection.Close();
            }

        }        
    }
}
