using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShipmentSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Shipments",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 7, 15, 42, 44, 182, DateTimeKind.Utc).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "ROLES",
                keyColumn: "role_id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 7, 15, 42, 44, 182, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 4, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(147));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 5, 3, 42, 44, 380, DateTimeKind.Utc).AddTicks(153));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 6, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 2, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(158));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 3, 3, 42, 44, 380, DateTimeKind.Utc).AddTicks(161));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 4, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(179));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 6, 3, 42, 44, 380, DateTimeKind.Utc).AddTicks(182));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 6, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(184));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 6, 15, 42, 44, 380, DateTimeKind.Utc).AddTicks(186));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 3, 7, 9, 42, 44, 380, DateTimeKind.Utc).AddTicks(188));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate", "ServiceType" },
                values: new object[] { new DateTime(2025, 3, 4, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 3, 9, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9718), "Standard" });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate", "ServiceType" },
                values: new object[] { new DateTime(2025, 3, 2, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9727), new DateTime(2025, 3, 6, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9728), "Standard" });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate", "ServiceType" },
                values: new object[] { new DateTime(2025, 3, 6, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9731), new DateTime(2025, 3, 11, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9732), "Standard" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 7, 15, 42, 44, 182, DateTimeKind.Utc).AddTicks(5262), "AQAAAAIAAYagAAAAEGTYy//H6mVUfhAYLJamiCHHBDSQwCSpEhu61f8y3oUfrZJGGOZvWMVV0lZfo8JbBw==" });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 3, 7, 15, 42, 44, 182, DateTimeKind.Utc).AddTicks(5265), "AQAAAAIAAYagAAAAEI5ZVAcYEVJ2xskbKJ9L/MxlN8dhubXsa7vT39UuvMSKX0y1wGQDX9Smq+hZud8JXg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Shipments");

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

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 24, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3275));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3277));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 21, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3279));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 22, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3281));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3283));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 25, 9, 5, 42, 77, DateTimeKind.Utc).AddTicks(3285));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3288));

            migrationBuilder.UpdateData(
                table: "ShipmentStatuses",
                keyColumn: "StatusId",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 2, 26, 15, 5, 42, 77, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 2, 23, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3212), new DateTime(2025, 2, 28, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 2, 21, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3231), new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3232) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 2, 25, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3235), new DateTime(2025, 3, 2, 21, 5, 42, 77, DateTimeKind.Utc).AddTicks(3235) });

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
        }
    }
}
