using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class Fixage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoEleve",
                schema: "db_accessadmin");

            migrationBuilder.DropColumn(
                name: "emailTokenExpiration",
                schema: "db_accessadmin",
                table: "Utilisateur");

            migrationBuilder.CreateTable(
                name: "PhotoUtilisateur",
                schema: "db_accessadmin",
                columns: table => new
                {
                    noPhoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUtilisateur = table.Column<int>(type: "int", nullable: false),
                    sourcePhoto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhotoUti__746C0A8C4075D9BF", x => x.noPhoto);
                    table.ForeignKey(
                        name: "FK__PhotoUtil__idUti__6DCC4D03",
                        column: x => x.idUtilisateur,
                        principalSchema: "db_accessadmin",
                        principalTable: "Utilisateur",
                        principalColumn: "idUtilisateur");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUtilisateur_idUtilisateur",
                schema: "db_accessadmin",
                table: "PhotoUtilisateur",
                column: "idUtilisateur");
        }
    }
}
