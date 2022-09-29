
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Core;


namespace Assignment.Infrastructure;




    public partial class KabanContext : DbContext{
        public KabanContext(DbContextOptions<KabanContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Tag> Tags {get; set;} =null!;
        public virtual DbSet<WorkItem> WorkItems {get; set;} =null!;
        public virtual DbSet<User> Users {get; set;} =null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Tag>(entity => {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            
            
            modelBuilder.Entity<WorkItem>(entity => {
                // State is stored as string
                entity.Property(e => e.State)
                 .HasConversion(
                    v => v.ToString(),
                    v => (State)Enum.Parse(typeof(State), v)
                ); 
            });
            
            

            modelBuilder.Entity<User>(entity => {
                entity.Property(e => e.Name).HasMaxLength(100);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    internal class KabanContextFactory : IDesignTimeDbContextFactory<KabanContext>
    {
        KabanContext IDesignTimeDbContextFactory<KabanContext>.CreateDbContext(string[] args){
        var configuration = new ConfigurationBuilder().AddUserSecrets<KabanContext>().Build();
        var connectionString = configuration.GetConnectionString("Kaban");

        var optionsBuilder = new DbContextOptionsBuilder<KabanContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new KabanContext(optionsBuilder.Options);
        }
    }

    

    








