using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class varcharDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Utilisateur ALTER COLUMN date_naissance VARCHAR(50);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
