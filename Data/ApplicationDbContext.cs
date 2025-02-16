using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using L_Connect.Models.Domain;
using Microsoft.AspNetCore.Identity;//for hashing password

namespace L_Connect.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial roles
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    RoleName = "ADMIN",
                    Description = "Administrator",
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "CLIENT",
                    Description = "Client User",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Create admin user
            var adminUser = new User
            {
                UserId = 1,
                Email = "admin@test.com",
                FullName = "Test Admin",
                ContactNumber = "1234567890",
                RoleId = 1, // Admin role
                CreatedAt = DateTime.UtcNow
            };
            // Create client user
            var clientUser = new User
            {
                UserId = 2,
                Email = "client@test.com",
                FullName = "Test Client",
                ContactNumber = "0987654321",
                RoleId = 2, // Client role
                CreatedAt = DateTime.UtcNow
            };

            // Hash the password
            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "admin123");
            clientUser.PasswordHash = _passwordHasher.HashPassword(clientUser, "client123");

            // Seed both users
            modelBuilder.Entity<User>().HasData(adminUser, clientUser);
        }
    }
}