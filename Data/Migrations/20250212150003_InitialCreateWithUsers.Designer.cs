﻿// <auto-generated />
using System;
using L_Connect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace L_Connect.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250212150003_InitialCreateWithUsers")]
    partial class InitialCreateWithUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

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
                            CreatedAt = new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7661),
                            Description = "Administrator",
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            RoleId = 2,
                            CreatedAt = new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7667),
                            Description = "Client User",
                            RoleName = "CLIENT"
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
                            CreatedAt = new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7891),
                            Email = "admin@test.com",
                            FullName = "Test Admin",
                            PasswordHash = "AQAAAAIAAYagAAAAEEJAwXbmUfSYYJGyyA1e3MRGOGCgoVt1b0tRfpmd2deBfde9ofOynYAkyEzGucaPNw==",
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            ContactNumber = "0987654321",
                            CreatedAt = new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7895),
                            Email = "client@test.com",
                            FullName = "Test Client",
                            PasswordHash = "AQAAAAIAAYagAAAAEDIXsG6wVg2O5omgDTZhVsRQ5vhensW+2kOzVX8MhAhKMBcfD9+A4Qgot8L1Zru2qw==",
                            RoleId = 2
                        });
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

            modelBuilder.Entity("L_Connect.Models.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
