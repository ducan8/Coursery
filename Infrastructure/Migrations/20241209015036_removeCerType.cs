using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeCerType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_CertificateTypes_CertificateTypeId",
                table: "Certificates");

            migrationBuilder.DropTable(
                name: "CertificateTypes");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_CertificateTypeId",
                table: "Certificates");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4e77ddcb-934d-45d1-a323-4517911e4308"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("800c3916-d906-48dd-ad40-e73041506d2a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c5c7bced-9d09-4b31-9050-781da5c6ee63"));

            migrationBuilder.DropColumn(
                name: "CertificateTypeId",
                table: "Certificates");

            migrationBuilder.AddColumn<string>(
                name: "CertificateType",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateType",
                table: "Certificates");

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateTypeId",
                table: "Certificates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CertificateTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { new Guid("4e77ddcb-934d-45d1-a323-4517911e4308"), "Admin", "Administrator" },
                    { new Guid("800c3916-d906-48dd-ad40-e73041506d2a"), "User", "User" },
                    { new Guid("c5c7bced-9d09-4b31-9050-781da5c6ee63"), "Teacher", "Teacher" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CertificateTypeId",
                table: "Certificates",
                column: "CertificateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_CertificateTypes_CertificateTypeId",
                table: "Certificates",
                column: "CertificateTypeId",
                principalTable: "CertificateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
