using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addImageAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Certificates_CertificateId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_CertificateId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_Image_OwnerId",
                table: "Image",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Certificates_OwnerId",
                table: "Image",
                column: "OwnerId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Certificates_OwnerId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_OwnerId",
                table: "Image");

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateId",
                table: "Image",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_CertificateId",
                table: "Image",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Certificates_CertificateId",
                table: "Image",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id");
        }
    }
}
