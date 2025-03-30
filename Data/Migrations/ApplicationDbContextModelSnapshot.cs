﻿// <auto-generated />
using System;
using L_Connect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace L_Connect.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("L_Connect.Models.Domain.Document", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DocumentID"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DocumentTypeID")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FileSize")
                        .HasColumnType("int");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ShipmentID")
                        .HasColumnType("int");

                    b.Property<int>("UploadedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("DocumentID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("ShipmentID");

                    b.HasIndex("UploadedBy");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.DocumentType", b =>
                {
                    b.Property<int>("DocumentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DocumentTypeID"));

                    b.Property<string>("AllowedExtensions")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DocumentTypeID");

                    b.ToTable("DocumentTypes");

                    b.HasData(
                        new
                        {
                            DocumentTypeID = 1,
                            AllowedExtensions = ".pdf,.docx,.jpg,.png",
                            Description = "Transport document for cargo shipments",
                            TypeName = "Bill of Lading"
                        },
                        new
                        {
                            DocumentTypeID = 2,
                            AllowedExtensions = ".pdf,.docx,.xlsx",
                            Description = "Commercial transaction document",
                            TypeName = "Commercial Invoice"
                        },
                        new
                        {
                            DocumentTypeID = 3,
                            AllowedExtensions = ".pdf,.docx,.xlsx",
                            Description = "Detailed packaging information",
                            TypeName = "Packing List"
                        },
                        new
                        {
                            DocumentTypeID = 4,
                            AllowedExtensions = ".pdf,.jpg,.png",
                            Description = "Documents for customs clearance",
                            TypeName = "Customs Declaration"
                        },
                        new
                        {
                            DocumentTypeID = 5,
                            AllowedExtensions = ".pdf,.jpg,.png",
                            Description = "Confirmation of delivery receipt",
                            TypeName = "Proof of Delivery"
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Pricing", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("price_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PriceId"));

                    b.Property<decimal>("BaseRate")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("base_rate");

                    b.Property<decimal>("BulkDiscountRate")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("bulk_discount_rate");

                    b.Property<int>("BulkThreshold")
                        .HasColumnType("int")
                        .HasColumnName("bulk_threshold");

                    b.Property<decimal>("CustomServiceCharge")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("custom_service_charge");

                    b.Property<string>("DestinationLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("destination_location");

                    b.Property<decimal>("InsuranceCharge")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("insurance_charge");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_updated_at");

                    b.Property<int?>("LastUpdatedBy")
                        .HasColumnType("int")
                        .HasColumnName("last_updated_by");

                    b.Property<string>("OriginLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("origin_location");

                    b.Property<string>("TransportationMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("transportation_method");

                    b.Property<decimal>("WeightRate")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("weight_rate");

                    b.HasKey("PriceId");

                    b.HasIndex("LastUpdatedBy");

                    b.ToTable("PRICING");

                    b.HasData(
                        new
                        {
                            PriceId = 1,
                            BaseRate = 250m,
                            BulkDiscountRate = 0.10m,
                            BulkThreshold = 10,
                            CustomServiceCharge = 50m,
                            DestinationLocation = "Sao Paulo",
                            InsuranceCharge = 20m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4017),
                            OriginLocation = "Shenzhen",
                            TransportationMethod = "Flight",
                            WeightRate = 5m
                        },
                        new
                        {
                            PriceId = 2,
                            BaseRate = 180m,
                            BulkDiscountRate = 0.10m,
                            BulkThreshold = 10,
                            CustomServiceCharge = 50m,
                            DestinationLocation = "Sao Paulo",
                            InsuranceCharge = 20m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4020),
                            OriginLocation = "Shenzhen",
                            TransportationMethod = "Sea",
                            WeightRate = 3m
                        },
                        new
                        {
                            PriceId = 3,
                            BaseRate = 290m,
                            BulkDiscountRate = 0.15m,
                            BulkThreshold = 12,
                            CustomServiceCharge = 55m,
                            DestinationLocation = "Toronto",
                            InsuranceCharge = 30m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4023),
                            OriginLocation = "Shenzhen",
                            TransportationMethod = "Flight",
                            WeightRate = 5m
                        },
                        new
                        {
                            PriceId = 4,
                            BaseRate = 210m,
                            BulkDiscountRate = 0.15m,
                            BulkThreshold = 12,
                            CustomServiceCharge = 55m,
                            DestinationLocation = "Toronto",
                            InsuranceCharge = 30m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4025),
                            OriginLocation = "Shenzhen",
                            TransportationMethod = "Sea",
                            WeightRate = 3m
                        },
                        new
                        {
                            PriceId = 5,
                            BaseRate = 150m,
                            BulkDiscountRate = 0.10m,
                            BulkThreshold = 10,
                            CustomServiceCharge = 40m,
                            DestinationLocation = "Canada",
                            InsuranceCharge = 15m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4027),
                            OriginLocation = "USA",
                            TransportationMethod = "Flight",
                            WeightRate = 4m
                        },
                        new
                        {
                            PriceId = 6,
                            BaseRate = 120m,
                            BulkDiscountRate = 0.10m,
                            BulkThreshold = 10,
                            CustomServiceCharge = 40m,
                            DestinationLocation = "Canada",
                            InsuranceCharge = 15m,
                            IsActive = true,
                            LastUpdatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4030),
                            OriginLocation = "USA",
                            TransportationMethod = "Sea",
                            WeightRate = 2.5m
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.ToTable("ROLES");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            CreatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(902),
                            Description = "Administrator",
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            RoleId = 2,
                            CreatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(905),
                            Description = "Client User",
                            RoleName = "CLIENT"
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Shipment", b =>
                {
                    b.Property<int>("ShipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ShipmentId"));

                    b.Property<int?>("AppliedPriceId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CreatedByAdminId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentLocation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DestinationAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EstimatedDeliveryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("FinalCost")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("OriginAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("ShipmentId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CreatedByAdminId");

                    b.ToTable("Shipments");

                    b.HasData(
                        new
                        {
                            ShipmentId = 1,
                            CreatedAt = new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8111),
                            CurrentLocation = "Transit Hub, Midway City",
                            CurrentStatus = "In Transit",
                            DestinationAddress = "456 Receiver Ave, Destination City, DC 67890",
                            EstimatedDeliveryDate = new DateTime(2025, 4, 1, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8116),
                            FinalCost = 0m,
                            OriginAddress = "123 Sender St, Origin City, OC 12345",
                            ServiceType = "Standard",
                            TrackingNumber = "LC-2402-123456",
                            Weight = 0m
                        },
                        new
                        {
                            ShipmentId = 2,
                            CreatedAt = new DateTime(2025, 3, 25, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8120),
                            CurrentLocation = "Destination Town",
                            CurrentStatus = "Delivered",
                            DestinationAddress = "101 Receiver Dr, Destination Town, DT 78901",
                            EstimatedDeliveryDate = new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8120),
                            FinalCost = 0m,
                            OriginAddress = "789 Sender Blvd, Origin Town, OT 23456",
                            ServiceType = "Standard",
                            TrackingNumber = "LC-2402-234567",
                            Weight = 0m
                        },
                        new
                        {
                            ShipmentId = 3,
                            ClientId = 2,
                            CreatedAt = new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8122),
                            CurrentLocation = "Origin Warehouse",
                            CurrentStatus = "Processing",
                            DestinationAddress = "777 Client St, Client City, CC 89012",
                            EstimatedDeliveryDate = new DateTime(2025, 4, 3, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8123),
                            FinalCost = 0m,
                            OriginAddress = "555 Business Rd, Corporate City, CC 34567",
                            ServiceType = "Standard",
                            TrackingNumber = "LC-2402-345678",
                            Weight = 0m
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.ShipmentStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdatedByAdminId")
                        .HasColumnType("int");

                    b.HasKey("StatusId");

                    b.HasIndex("ShipmentId");

                    b.HasIndex("UpdatedByAdminId");

                    b.ToTable("ShipmentStatuses");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Location = "Online System",
                            Notes = "Shipment order received and entered into system",
                            ShipmentId = 1,
                            Status = "Order Received",
                            UpdatedAt = new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8146)
                        },
                        new
                        {
                            StatusId = 2,
                            Location = "Origin Warehouse",
                            Notes = "Package received at origin facility",
                            ShipmentId = 1,
                            Status = "Processing",
                            UpdatedAt = new DateTime(2025, 3, 27, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8148)
                        },
                        new
                        {
                            StatusId = 3,
                            Location = "Transit Hub, Midway City",
                            Notes = "Package in transit to destination",
                            ShipmentId = 1,
                            Status = "In Transit",
                            UpdatedAt = new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8149)
                        },
                        new
                        {
                            StatusId = 4,
                            Location = "Online System",
                            Notes = "Shipment order received",
                            ShipmentId = 2,
                            Status = "Order Received",
                            UpdatedAt = new DateTime(2025, 3, 25, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8151)
                        },
                        new
                        {
                            StatusId = 5,
                            Location = "Origin Warehouse",
                            Notes = "Package being processed",
                            ShipmentId = 2,
                            Status = "Processing",
                            UpdatedAt = new DateTime(2025, 3, 25, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8152)
                        },
                        new
                        {
                            StatusId = 6,
                            Location = "Transit Hub",
                            Notes = "Package in transit",
                            ShipmentId = 2,
                            Status = "In Transit",
                            UpdatedAt = new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8171)
                        },
                        new
                        {
                            StatusId = 7,
                            Location = "Local Delivery Center",
                            Notes = "Package out for delivery",
                            ShipmentId = 2,
                            Status = "Out for Delivery",
                            UpdatedAt = new DateTime(2025, 3, 28, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8172)
                        },
                        new
                        {
                            StatusId = 8,
                            Location = "Destination Town",
                            Notes = "Package delivered successfully",
                            ShipmentId = 2,
                            Status = "Delivered",
                            UpdatedAt = new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8173)
                        },
                        new
                        {
                            StatusId = 9,
                            Location = "Online System",
                            Notes = "Order received from client",
                            ShipmentId = 3,
                            Status = "Order Received",
                            UpdatedAt = new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8174)
                        },
                        new
                        {
                            StatusId = 10,
                            Location = "Origin Warehouse",
                            Notes = "Package being prepared for shipping",
                            ShipmentId = 3,
                            Status = "Processing",
                            UpdatedAt = new DateTime(2025, 3, 29, 23, 0, 13, 272, DateTimeKind.Utc).AddTicks(8176)
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("contact_number");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("full_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("password_hash");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("USERS");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            ContactNumber = "1234567890",
                            CreatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(992),
                            Email = "admin@test.com",
                            FullName = "Test Admin",
                            PasswordHash = "AQAAAAIAAYagAAAAEIOfA1qdJtlmNlKYsecdeQ7lEIKszCglOcEPRTV/ImGJZ6g6JCRyKFieCXukn7ZGIg==",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            ContactNumber = "0987654321",
                            CreatedAt = new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(994),
                            Email = "client@test.com",
                            FullName = "Test Client",
                            PasswordHash = "AQAAAAIAAYagAAAAEPW9O97q9/dAIZfM1sGzmTzwqbV0KqfwBsr1+dDK4/oYljPGrHi5Gv+FA2pj+0sPDw==",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Document", b =>
                {
                    b.HasOne("L_Connect.Models.Domain.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("L_Connect.Models.Domain.Shipment", "Shipment")
                        .WithMany("Documents")
                        .HasForeignKey("ShipmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("L_Connect.Models.Domain.User", "UploadedByUser")
                        .WithMany()
                        .HasForeignKey("UploadedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Shipment");

                    b.Navigation("UploadedByUser");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Pricing", b =>
                {
                    b.HasOne("L_Connect.Models.Domain.User", "UpdatedByUser")
                        .WithMany()
                        .HasForeignKey("LastUpdatedBy");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Shipment", b =>
                {
                    b.HasOne("L_Connect.Models.Domain.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("L_Connect.Models.Domain.User", "Admin")
                        .WithMany()
                        .HasForeignKey("CreatedByAdminId");

                    b.Navigation("Admin");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.ShipmentStatus", b =>
                {
                    b.HasOne("L_Connect.Models.Domain.Shipment", "Shipment")
                        .WithMany()
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("L_Connect.Models.Domain.User", "Admin")
                        .WithMany()
                        .HasForeignKey("UpdatedByAdminId");

                    b.Navigation("Admin");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.User", b =>
                {
                    b.HasOne("L_Connect.Models.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.DocumentType", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("L_Connect.Models.Domain.Shipment", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
