using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRICING",
                columns: table => new
                {
                    price_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    origin_location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    destination_location = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transportation_method = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    weight_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    bulk_threshold = table.Column<int>(type: "int", nullable: false),
                    bulk_discount_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    custom_service_charge = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    insurance_charge = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    last_updated_by = table.Column<int>(type: "int", nullable: true),
                    last_updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRICING", x => x.price_id);
                    table.ForeignKey(
                        name: "FK_PRICING_USERS_last_updated_by",
                        column: x => x.last_updated_by,
                        principalTable: "USERS",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PRICING",
                columns: new[] { "price_id", "base_rate", "bulk_discount_rate", "bulk_threshold", "custom_service_charge", "destination_location", "insurance_charge", "is_active", "last_updated_at", "last_updated_by", "origin_location", "transportation_method", "weight_rate" },
                values: new object[,]
                {
                    { 1, 250m, 0.10m, 10, 50m, "Sao Paulo", 20m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4017), null, "Shenzhen", "Flight", 5m },
                    { 2, 180m, 0.10m, 10, 50m, "Sao Paulo", 20m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4020), null, "Shenzhen", "Sea", 3m },
                    { 3, 290m, 0.15m, 12, 55m, "Toronto", 30m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4023), null, "Shenzhen", "Flight", 5m },
                    { 4, 210m, 0.15m, 12, 55m, "Toronto", 30m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4025), null, "Shenzhen", "Sea", 3m },
                    { 5, 150m, 0.10m, 10, 40m, "Canada", 15m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4027), null, "USA", "Flight", 4m },
                    { 6, 120m, 0.10m, 10, 40m, "Canada", 15m, true, new DateTime(2025, 3, 30, 5, 0, 13, 273, DateTimeKind.Utc).AddTicks(4030), null, "USA", "Sea", 2.5m }
                });

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(902));

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(905));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8146));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 27, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8148));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8149));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 25, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 25, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8171));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 28, 17, 0, 13, 272, DateTimeKind.Utc).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8173));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8174));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 29, 23, 0, 13, 272, DateTimeKind.Utc).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 27, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8111), new DateTime(2025, 4, 1, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8116) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 25, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8120), new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8120) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 29, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8122), new DateTime(2025, 4, 3, 5, 0, 13, 272, DateTimeKind.Utc).AddTicks(8123) });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(992), "AQAAAAIAAYagAAAAEIOfA1qdJtlmNlKYsecdeQ7lEIKszCglOcEPRTV/ImGJZ6g6JCRyKFieCXukn7ZGIg==" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 30, 5, 0, 13, 190, DateTimeKind.Utc).AddTicks(994), "AQAAAAIAAYagAAAAEPW9O97q9/dAIZfM1sGzmTzwqbV0KqfwBsr1+dDK4/oYljPGrHi5Gv+FA2pj+0sPDw==" });

            migrationBuilder.CreateIndex(
                name: "IX_PRICING_last_updated_by",
                table: "PRICING",
                column: "last_updated_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRICING");

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 17, 22, 31, 37, 280, DateTimeKind.Utc).AddTicks(1402));

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 17, 22, 31, 37, 280, DateTimeKind.Utc).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 14, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 15, 10, 31, 37, 364, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 16, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 12, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 13, 10, 31, 37, 364, DateTimeKind.Utc).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 14, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 16, 10, 31, 37, 364, DateTimeKind.Utc).AddTicks(7485));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 16, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7486));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 16, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7487));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 17, 16, 31, 37, 364, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 14, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7422), new DateTime(2025, 3, 19, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7426) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 12, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7431), new DateTime(2025, 3, 16, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7431) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 16, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7433), new DateTime(2025, 3, 21, 22, 31, 37, 364, DateTimeKind.Utc).AddTicks(7434) });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 17, 22, 31, 37, 280, DateTimeKind.Utc).AddTicks(1509), "AQAAAAIAAYagAAAAEIRF+5CYq8SnUi4NcpLbhZKDWiLEUp27doWYdc8ZaXdjQGopi4YJlv0hvb8bfoPQcg==" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 17, 22, 31, 37, 280, DateTimeKind.Utc).AddTicks(1511), "AQAAAAIAAYagAAAAEHRnigGo1MHnHjp8CBfl/ARCBbKrkls79wfShiRMmLLnyITRcDWOyK1WveyaAvvFzg==" });
        }
    }
}
