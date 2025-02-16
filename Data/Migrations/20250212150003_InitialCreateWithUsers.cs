using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace L_Connect.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.role_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_USERS_ROLES_role_id",
                        column: x => x.role_id,
                        principalTable: "ROLES",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "role_id", "created_at", "description", "role_name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7661), "Administrator", "ADMIN" },
                    { 2, new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7667), "Client User", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "user_id", "contact_number", "created_at", "email", "full_name", "password_hash", "role_id" },
                values: new object[,]
                {
                    { 1, "1234567890", new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7891), "admin@test.com", "Test Admin", "AQAAAAIAAYagAAAAEEJAwXbmUfSYYJGyyA1e3MRGOGCgoVt1b0tRfpmd2deBfde9ofOynYAkyEzGucaPNw==", 1 },
                    { 2, "0987654321", new DateTime(2025, 2, 12, 15, 0, 2, 103, DateTimeKind.Utc).AddTicks(7895), "client@test.com", "Test Client", "AQAAAAIAAYagAAAAEDIXsG6wVg2O5omgDTZhVsRQ5vhensW+2kOzVX8MhAhKMBcfD9+A4Qgot8L1Zru2qw==", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_role_id",
                table: "USERS",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
