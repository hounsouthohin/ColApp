using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "DateExpiration",
            table: "SeSouvenirTokens",
            type: "NVARCHAR(60)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "DATETIME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Annuler la modification et revenir à DATETIME si nécessaire
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateExpiration",
                table: "SeSouvenirTokens",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(60)");
        }
    }
}
