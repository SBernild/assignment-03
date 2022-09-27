
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.UserSecrets;


namespace Assignment.Infrastructure;




    public partial class KabanContext : DbContext{
        public KabanContext(DbContextOptions<KabanContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Tag> Tags {get; set;} =null!;
        public virtual DbSet<WorkItem> Tasks {get; set;} =null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Tag>(entity => {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<WorkItem>(entity => {
                entity.Property(e => e.Title);
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

    

    








