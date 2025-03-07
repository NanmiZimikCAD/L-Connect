using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using L_Connect.Models.Domain;
using Microsoft.AspNetCore.Identity;//for hashing password

// ApplicationDbContext sets up the database structure and provides initial test data for the system. It ensures we have the necessary roles and test accounts ready as soon as the application is deployed
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

        //The database context setup with DbSet properties establishes the connection between your entity models and the database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }

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

            // Seed shipments
            modelBuilder.Entity<Shipment>().HasData(
                new Shipment
                {
                    ShipmentId = 1,
                    TrackingNumber = "LC-2402-123456",
                    ClientId = null, // Guest shipment
                    OriginAddress = "123 Sender St, Origin City, OC 12345",
                    DestinationAddress = "456 Receiver Ave, Destination City, DC 67890",
                    CurrentStatus = "In Transit",
                    CurrentLocation = "Transit Hub, Midway City",
                    ServiceType = "Standard",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2)
                },
                new Shipment
                {
                    ShipmentId = 2,
                    TrackingNumber = "LC-2402-234567",
                    ClientId = null, // Guest shipment
                    OriginAddress = "789 Sender Blvd, Origin Town, OT 23456",
                    DestinationAddress = "101 Receiver Dr, Destination Town, DT 78901",
                    CurrentStatus = "Delivered",
                    CurrentLocation = "Destination Town",
                    ServiceType = "Standard",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(-1)
                },
                new Shipment
                {
                    ShipmentId = 3,
                    TrackingNumber = "LC-2402-345678",
                    ClientId = 2, // The client user you created
                    OriginAddress = "555 Business Rd, Corporate City, CC 34567",
                    DestinationAddress = "777 Client St, Client City, CC 89012",
                    CurrentStatus = "Processing",
                    CurrentLocation = "Origin Warehouse",
                    ServiceType = "Standard",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(4)
                }
            );

            // Seed shipment statuses
            modelBuilder.Entity<ShipmentStatus>().HasData(
                // First shipment statuses (In Transit)
                new ShipmentStatus
                {
                    StatusId = 1,
                    ShipmentId = 1,
                    Status = "Order Received",
                    Location = "Online System",
                    Notes = "Shipment order received and entered into system",
                    UpdatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new ShipmentStatus
                {
                    StatusId = 2,
                    ShipmentId = 1,
                    Status = "Processing",
                    Location = "Origin Warehouse",
                    Notes = "Package received at origin facility",
                    UpdatedAt = DateTime.UtcNow.AddDays(-2.5)
                },
                new ShipmentStatus
                {
                    StatusId = 3,
                    ShipmentId = 1,
                    Status = "In Transit",
                    Location = "Transit Hub, Midway City",
                    Notes = "Package in transit to destination",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                },
                
                // Second shipment statuses (Delivered)
                new ShipmentStatus
                {
                    StatusId = 4,
                    ShipmentId = 2,
                    Status = "Order Received",
                    Location = "Online System",
                    Notes = "Shipment order received",
                    UpdatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new ShipmentStatus
                {
                    StatusId = 5,
                    ShipmentId = 2,
                    Status = "Processing",
                    Location = "Origin Warehouse",
                    Notes = "Package being processed",
                    UpdatedAt = DateTime.UtcNow.AddDays(-4.5)
                },
                new ShipmentStatus
                {
                    StatusId = 6,
                    ShipmentId = 2,
                    Status = "In Transit",
                    Location = "Transit Hub",
                    Notes = "Package in transit",
                    UpdatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new ShipmentStatus
                {
                    StatusId = 7,
                    ShipmentId = 2,
                    Status = "Out for Delivery",
                    Location = "Local Delivery Center",
                    Notes = "Package out for delivery",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1.5)
                },
                new ShipmentStatus
                {
                    StatusId = 8,
                    ShipmentId = 2,
                    Status = "Delivered",
                    Location = "Destination Town",
                    Notes = "Package delivered successfully",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                },
                
                // Third shipment statuses (Processing)
                new ShipmentStatus
                {
                    StatusId = 9,
                    ShipmentId = 3,
                    Status = "Order Received",
                    Location = "Online System",
                    Notes = "Order received from client",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new ShipmentStatus
                {
                    StatusId = 10,
                    ShipmentId = 3,
                    Status = "Processing",
                    Location = "Origin Warehouse",
                    Notes = "Package being prepared for shipping",
                    UpdatedAt = DateTime.UtcNow.AddHours(-6)
                }
            );
        }
    }
}