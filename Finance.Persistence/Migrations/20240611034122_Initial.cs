using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Category = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar", maxLength: 1, nullable: false),
                    Currency = table.Column<string>(type: "varchar", maxLength: 2, nullable: false),
                    Recurring = table.Column<bool>(type: "boolean", nullable: false),
                    FrequencyInDays = table.Column<int>(type: "integer", maxLength: 3, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_expenses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expenses_UserId",
                table: "expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
