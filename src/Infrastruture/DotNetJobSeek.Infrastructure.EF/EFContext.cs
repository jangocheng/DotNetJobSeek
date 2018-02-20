using System;
using DotNetJobSeek.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace DotNetJobSeek.Infrastructure.EF
{
    public class EFContext : DbContext
    {

        public EFContext()
        { }
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // set default connection string
            if(!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlite(@"DataSource=../../mydb.db");
                optionsBuilder.UseNpgsql("Username=postgres;Password=hellopassword;Host=localhost;Port=5432;Database=jobseek;Pooling=true;", providerOptions=>providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagMapper());
            modelBuilder.ApplyConfiguration(new KeywordMapper());
            modelBuilder.ApplyConfiguration(new TagKeywordMapper());
        }

        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("aa");
        }
    }
}
