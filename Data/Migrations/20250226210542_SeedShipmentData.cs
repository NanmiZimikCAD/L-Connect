using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedShipmentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrackingNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CreatedByAdminId = table.Column<int>(type: "int", nullable: true),
                    AppliedPriceId = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OriginAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FinalCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_USERS_ClientId",
                        column: x => x.ClientId,
                        principalTable: "USERS",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Shipments_USERS_CreatedByAdminId",
                        column: x => x.CreatedByAdminId,
                        principalTable: "USERS",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShipmentStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    UpdatedByAdminId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_ShipmentStatuses_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentStatuses_USERS_UpdatedByAdminId",
                        column: x => x.UpdatedByAdminId,
                        principalTable: "USERS",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 26, 21, 5, 41, 992, DateTimeKind.Utc).AddTicks(8216));

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 26, 21, 5, 41, 992, DateTimeKind.Utc).AddTicks(8285));

            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "ShipmentId", "AppliedPriceId", "ClientId", "CreatedAt", "CreatedByAdminId", "CurrentLocation", "CurrentStatus", "DestinationAddress", "EstimatedDeliveryDate", "FinalCost", "OriginAddress", "TrackingNumber", "Weight" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3212), null, "Transit Hub, Midway City", "In Transit", "456 Receiver Ave, Destination City, DC 67890", new DateTime(2025, 2, 28, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3222), 0m, "123 Sender St, Origin City, OC 12345", "LC-2402-123456", 0m },
                    { 2, null, null, new DateTime(2025, 2, 21, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3231), null, "Destination Town", "Delivered", "101 Receiver Dr, Destination Town, DT 78901", new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3232), 0m, "789 Sender Blvd, Origin Town, OT 23456", "LC-2402-234567", 0m },
                    { 3, null, 2, new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3235), null, "Origin Warehouse", "Processing", "777 Client St, Client City, CC 89012", new DateTime(2025, 3, 2, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3235), 0m, "555 Business Rd, Corporate City, CC 34567", "LC-2402-345678", 0m }
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 2, 26, 21, 5, 41, 992, DateTimeKind.Utc).AddTicks(8594), "AQAAAAIAAYagAAAAEMtBDC/lTQ03NlRRzcI422Q8MccJrAFkaIv7cdkN/+hnx5bPa3xe08DVG+KjXI8SbQ==" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 2, 26, 21, 5, 41, 992, DateTimeKind.Utc).AddTicks(8596), "AQAAAAIAAYagAAAAELa9uBxn8IA28oIRnr5E83B0uNP3kWM5VOFzgy1irrtuiX5/LTtooFJJvyzKNjUfQg==" });

            migrationBuilder.InsertData(
                table: "ShipmentStatuses",
                columns: new[] { "StatusId", "Location", "Notes", "ShipmentId", "Status", "UpdatedAt", "UpdatedByAdminId" },
                values: new object[,]
                {
                    { 1, "Online System", "Shipment order received and entered into system", 1, "Order Received", new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3271), null },
                    { 2, "Origin Warehouse", "Package received at origin facility", 1, "Processing", new DateTime(2025, 2, 24, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3275), null },
                    { 3, "Transit Hub, Midway City", "Package in transit to destination", 1, "In Transit", new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3277), null },
                    { 4, "Online System", "Shipment order received", 2, "Order Received", new DateTime(2025, 2, 21, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3279), null },
                    { 5, "Origin Warehouse", "Package being processed", 2, "Processing", new DateTime(2025, 2, 22, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3281), null },
                    { 6, "Transit Hub", "Package in transit", 2, "In Transit", new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3283), null },
                    { 7, "Local Delivery Center", "Package out for delivery", 2, "Out for Delivery", new DateTime(2025, 2, 25, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3285), null },
                    { 8, "Destination Town", "Package delivered successfully", 2, "Delivered", new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3287), null },
                    { 9, "Online System", "Order received from client", 3, "Order Received", new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3288), null },
                    { 10, "Origin Warehouse", "Package being prepared for shipping", 3, "Processing", new DateTime(2025, 2, 26, 15, 5, 42, 77, DateTimeKind.Utc).AddTicks(3290), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ClientId",
                table: "Shipments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CreatedByAdminId",
                table: "Shipments",
                column: "CreatedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatuses_ShipmentId",
                table: "ShipmentStatuses",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatuses_UpdatedByAdminId",
                table: "ShipmentStatuses",
                column: "UpdatedByAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentStatuses");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7661));

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7891), "AQAAAAIAAYagAAAAEEJAwXbmUfSYYJGyyA1e3MRGOGCgoVt1b0tRfpmd2deBfde9ofOynYAkyEzGucaPNw==" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7895), "AQAAAAIAAYagAAAAEDIXsG6wVg2O5omgDTZhVsRQ5vhensW+2kOzVX8MhAhKMBcfD9+A4Qgot8L1Zru2qw==" });
        }
    }
}
