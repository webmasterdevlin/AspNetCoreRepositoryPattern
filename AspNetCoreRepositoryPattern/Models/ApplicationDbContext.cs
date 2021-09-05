using System;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Done)
                .IsRequired();
        }

    }
}
