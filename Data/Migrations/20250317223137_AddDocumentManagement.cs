using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowedExtensions = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipmentID = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalFileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Shipments_ShipmentID",
                        column: x => x.ShipmentID,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_USERS_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "DocumentTypeID", "AllowedExtensions", "Description", "TypeName" },
                values: new object[,]
                {
                    { 1, ".pdf,.docx,.jpg,.png", "Transport document for cargo shipments", "Bill of Lading" },
                    { 2, ".pdf,.docx,.xlsx", "Commercial transaction document", "Commercial Invoice" },
                    { 3, ".pdf,.docx,.xlsx", "Detailed packaging information", "Packing List" },
                    { 4, ".pdf,.jpg,.png", "Documents for customs clearance", "Customs Declaration" },
                    { 5, ".pdf,.jpg,.png", "Confirmation of delivery receipt", "Proof of Delivery" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeID",
                table: "Documents",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ShipmentID",
                table: "Documents",
                column: "ShipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadedBy",
                table: "Documents",
                column: "UploadedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

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
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 4, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 3, 9, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9718) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 2, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9727), new DateTime(2025, 3, 6, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9728) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 3, 6, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9731), new DateTime(2025, 3, 11, 15, 42, 44, 379, DateTimeKind.Utc).AddTicks(9732) });

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
    }
}
