using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { new Guid("2f42ddbb-1f3b-474d-91b1-763b3e8e5c4d"), "Admin", "Administrator" },
                    { new Guid("35c00b89-57c0-412c-8014-716025e59b31"), "User", "User" },
                    { new Guid("dc370e68-6972-480a-aa37-f88d2c046312"), "Teacher", "Teacher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2f42ddbb-1f3b-474d-91b1-763b3e8e5c4d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("35c00b89-57c0-412c-8014-716025e59b31"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dc370e68-6972-480a-aa37-f88d2c046312"));
        }
    }
}
