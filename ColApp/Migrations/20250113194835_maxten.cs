using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class maxten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
            name: "TentativesConnexion",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Courriel = table.Column<string>(type: "nvarchar(65)", nullable: false),
                Tentatives = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                DateDeblocage = table.Column<DateTime>(type: "datetime", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TentativesConnexion", x => x.Id);
            });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "TentativesConnexion");

        }
    }
}
