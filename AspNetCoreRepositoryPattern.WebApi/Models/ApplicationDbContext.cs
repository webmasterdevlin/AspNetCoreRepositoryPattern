using System;
using AspNetCoreRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasData(
                new Todo
                {
                    Id = new Guid("a7c222b7-7e82-4a01-8085-b5abc191f777"),
                    Done = true,
                    Name = "Shopping",
                    CreatedAt = DateTime.Now
                },
                new Todo
                {
                    Id = new Guid("90d4e85a-54d8-48a9-b3a8-63d5560ea4bc"),
                    Done = true,
                    Name = "Cleaning",
                    CreatedAt = DateTime.Now
                },
                new Todo
                {
                    Id = new Guid("892ed1f0-52bc-455f-999a-c3be73790dfa"),
                    Done = true,
                    Name = "Coding",
                    CreatedAt = DateTime.Now
                },
                new Todo
                {
                    Id = new Guid("cfa7b2b2-08a0-4361-9dce-fe2dbe789190"),
                    Done = true,
                    Name = "Travelling",
                    CreatedAt = DateTime.Now
                }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("a560d8e6-ed98-42cf-8556-c5c12a553c8d"),
                    Title = "In Search of Lost Time",
                    Description = "Swanns Way, the first part of A la recherche de temps perdu, Marcel Prousts seven-part cycle, was published in 1913.",
                    Author = "Marcel Proust",
                    CreatedAt = DateTime.Now
                },
                new Book
                {
                    Id = new Guid("9175edd8-6ee3-426f-83b6-9d3d90fd8116"),
                    Title = "Ulysses",
                    Description = "Ulysses chronicles the passage of Leopold Bloom through Dublin during an ordinary day, June 16, 1904.",
                    Author = "James Joyce",
                    CreatedAt = DateTime.Now
                },
                new Book
                {
                    Id = new Guid("457d57c6-8514-4d5c-b0b7-d72246df5dea"),
                    Title = "Don Quixote",
                    Description = "Alonso Quixano, a retired country gentleman in his fifties, lives in an unnamed section of La Mancha with his niece and a housekeeper.",
                    Author = "Miguel de Cervantes",
                    CreatedAt = DateTime.Now
                },
                new Book
                {
                    Id = new Guid("3c1fd1eb-3ad3-4108-b236-b5275d140baa"),
                    Title = "One Hundred Years of Solitude",
                    Description = "One of the 20th century's enduring works, One Hundred Years of Solitude is a widely beloved and acclaimed novel known throughout the world...",
                    Author = "Gabriel Garcia Marquez",
                    CreatedAt = DateTime.Now
                }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = new Guid("b8c8b904-d7ed-48bf-90d7-297e412e2320"),
                    FirstName = "Jakob",
                    LastName = "Larsen",
                    Email = "jakoblarsen@gmail.com",
                    Mobile = "90901234",
                    DateOfBirth = DateTime.Today,
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    Id = new Guid("0826373d-c58d-4db3-957e-d83c7705a4b8"),
                    FirstName = "Nora",
                    LastName = "Olsen",
                    Email = "noraolsen@gmail.com",
                    Mobile = "90896745",
                    DateOfBirth = DateTime.Today,
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    Id = new Guid("0a3ecdf5-4685-4eab-811f-a2545777030a"),
                    FirstName = "Emil",
                    LastName = "Johansen",
                    Email = "emiljohansen@gmail.com",
                    Mobile = "90452378",
                    DateOfBirth = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    Id = new Guid("1758c6df-5d92-4eff-a5c0-f65c40c01621"),
                    FirstName = "Emma",
                    LastName = "Hansen",
                    Email = "emmahansen@gmail.com",
                    Mobile = "90129034",
                    DateOfBirth = DateTime.Today,
                    CreatedAt = DateTime.Now
                }
                );
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
