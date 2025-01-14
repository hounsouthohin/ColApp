using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class emailToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
        name: "IsEmailVerified",
        table: "Utilisateur",
        type: "bit",
        nullable: false,
        defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerifyEmailToken",
                table: "Utilisateur",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
        name: "IsEmailVerified",
        table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "VerifyEmailToken",
                table: "Utilisateur");

        }
    }
}
