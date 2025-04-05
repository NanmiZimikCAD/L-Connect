using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using L_Connect.Data;
using L_Connect.Models.Domain;
using System.Linq;

namespace L_Connect.Tests.TestUtilities
{
    public static class TestDataGenerator
    {
        public static void SeedTestData(ApplicationDbContext context)
        {
            // Clear all data using RemoveRange with local cache to avoid EF Core tracking issues
            ClearAllData(context);

            // Recreate Password Hasher
            var passwordHasher = new PasswordHasher<User>();

            // Seed Roles with checked existence
            SeedRoles(context, passwordHasher);

            // Save changes after each major seeding operation
            context.SaveChanges();
        }

        private static void ClearAllData(ApplicationDbContext context)
        {
            // Disable change tracking to prevent conflicts
            context.ChangeTracker.Clear();

            // Remove all data from tables
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void SeedRoles(ApplicationDbContext context, PasswordHasher<User> passwordHasher)
        {
            // Ensure roles are not already present
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
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
                };

                // Use AddRange to add multiple roles at once
                context.Roles.AddRange(roles);
            }

            // Seed Users with unique approach
            SeedUsers(context, passwordHasher);
        }

        private static void SeedUsers(ApplicationDbContext context, PasswordHasher<User> passwordHasher)
        {
            // Check if users already exist
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    UserId = 1,
                    Email = "admin@test.com",
                    FullName = "Test Admin",
                    ContactNumber = "1234567890",
                    RoleId = 1,
                    CreatedAt = DateTime.UtcNow
                };
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin123");

                var clientUser = new User
                {
                    UserId = 2,
                    Email = "client@test.com",
                    FullName = "Test Client",
                    ContactNumber = "0987654321",
                    RoleId = 2,
                    CreatedAt = DateTime.UtcNow
                };
                clientUser.PasswordHash = passwordHasher.HashPassword(clientUser, "client123");

                context.Users.AddRange(adminUser, clientUser);
            }

            // Seed Shipments
            SeedShipments(context);
        }

        private static void SeedShipments(ApplicationDbContext context)
        {
            // Check if shipments already exist
            if (!context.Shipments.Any())
            {
                var shipments = new List<Shipment>
                {
                    new Shipment
                    {
                        ShipmentId = 1,
                        TrackingNumber = "LC-2402-123456",
                        ClientId = null,
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
                        ClientId = null,
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
                        ClientId = 2,
                        OriginAddress = "555 Business Rd, Corporate City, CC 34567",
                        DestinationAddress = "777 Client St, Client City, CC 89012",
                        CurrentStatus = "Processing",
                        CurrentLocation = "Origin Warehouse",
                        ServiceType = "Standard",
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        EstimatedDeliveryDate = DateTime.UtcNow.AddDays(4)
                    }
                };

                context.Shipments.AddRange(shipments);
            }

            // Seed Shipment Statuses
            SeedShipmentStatuses(context);
        }

        private static void SeedShipmentStatuses(ApplicationDbContext context)
        {
            // Check if shipment statuses already exist
            if (!context.ShipmentStatuses.Any())
            {
                var shipmentStatuses = new List<ShipmentStatus>
                {
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
                    }
                };

                context.ShipmentStatuses.AddRange(shipmentStatuses);
            }
        }
    }
}