using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class UdatDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Utilisateur ALTER COLUMN date_naissance DATE;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
