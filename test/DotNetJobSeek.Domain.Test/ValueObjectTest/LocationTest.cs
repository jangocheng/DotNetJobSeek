using System;
using Xunit;
using DotNetJobSeek.Domain;
using Microsoft.Data.Sqlite;
using DotNetJobSeek.Infrastructure.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DotNetJobSeek.Domain.Test
{
    public class LocationTest
    {
        Location t1, t2, t3, t4, t5;
        Locality locality1;
        State s1;
        public LocationTest()
        {
            t1 = new Location { Geohash = "1", Address = "food" };
            t2 = new Location { Geohash = "2", Address = "meat" };
            t3 = new Location { Geohash = "3", Address = "drink" };
            t4 = new Location { Geohash = "4", Address = "meal" };
            t5 = new Location { Geohash = "5", Address = "dog" };
            locality1 = new Locality { Name = "Hobart", Postcode = "7000" };
            s1 = new State { Name = "Tasmania", Country = "Australia" };
        }
        [Fact]
        public void TestLocationsAdd()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Location test;
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
                    locality1.State = s1;
                    t1.Locality = locality1;
                    context.Locations.Add(t1);
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
                    test = context.Locations.Where(t => t.Geohash == "1")
                    .Include(l => l.Locality)
                        .ThenInclude(lct => lct.State)
                    .FirstOrDefault();
                }
                Assert.Equal("food", test.Address);
                Assert.Equal("Hobart", test.Locality.Name);
                Assert.Equal("7000", test.Locality.Postcode);
                Assert.Equal("Tasmania", test.Locality.State.Name);
            }
            finally
            {
                connection.Close();
            }

        }
        [Fact]
        public void TestLocationsUpdate()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            Location test;
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

                    locality1.State = s1;
                    t1.Locality = locality1;
                    context.Locations.Add(t1);
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
                    var testUpdate = context.Locations.Where(t => t.Geohash == "1")
                    .Include(l => l.Locality)
                        .ThenInclude(lct => lct.State)
                    .FirstOrDefault();
                    testUpdate.Address = "food1";
                    testUpdate.Locality.Name = "Midway";
                    testUpdate.Locality.Postcode = "7171";

                    testUpdate.Locality.State.Name = "Tas";
                    context.Locations.Update(testUpdate);
                    context.SaveChanges();
                }
                using(var context = new EFContext(options))
                {
                    test = context.Locations.Where(t => t.Geohash == "1")
                    .Include(l => l.Locality)
                        .ThenInclude(lct => lct.State)
                    .FirstOrDefault();
                }
                Assert.Equal("food1", test.Address);                
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
