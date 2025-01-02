using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "PasswordResetToken",
            table: "Utilisateur",
            type: "VARCHAR(100)",
            nullable: true);  // Nullable, car le token de réinitialisation peut ne pas être défini

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Utilisateur",
                type: "DATETIME",
                nullable: true);  // Nullable, car le token de réinitialisation peut ne pas avoir de date d'expiration définie
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "PasswordResetToken",
             table: "Utilisateur");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Utilisateur");
        }
    }
}
