using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "db_accessadmin");

            migrationBuilder.CreateTable(
                name: "Etablissement",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idEtablissement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomEtab = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    delaiInscription = table.Column<int>(type: "int", nullable: false),
                    contact = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    nomProviseur = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    prenomProviseur = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dateRentre = table.Column<DateTime>(type: "date", nullable: false),
                    province = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ville = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    secteur = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Etabliss__1E6C217E31806189", x => x.idEtablissement);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idNotifications = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__430D4E1ACD5D51A9", x => x.idNotifications);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    prenom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    courriel = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    motDePasse = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    sel = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    date_naissance = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilisat__5366DB197F114924", x => x.idUtilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idClasse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomClasse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nbEleves = table.Column<int>(type: "int", nullable: false),
                    idEtablissement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Classe__60FFF8016B1A79F7", x => x.idClasse);
                    table.ForeignKey(
                        name: "FK__Classe__idEtabli__5D95E53A",
                        column: x => x.idEtablissement,
                        principalSchema: "db_accessadmin",
                        principalTable: "Etablissement",
                        principalColumn: "idEtablissement");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idUtilisateur = table.Column<int>(type: "int", nullable: false),
                    idEtablissement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__8D0E911DBD983FD1", x => x.idMessage);
                    table.ForeignKey(
                        name: "FK__Message__idEtabl__6AEFE058",
                        column: x => x.idEtablissement,
                        principalSchema: "db_accessadmin",
                        principalTable: "Etablissement",
                        principalColumn: "idEtablissement");
                    table.ForeignKey(
                        name: "FK__Message__idUtili__69FBBC1F",
                        column: x => x.idUtilisateur,
                        principalSchema: "db_accessadmin",
                        principalTable: "Utilisateur",
                        principalColumn: "idUtilisateur");
                });

            migrationBuilder.CreateTable(
                name: "PhotoUtilisateur",
                schema: "db_accessadmin",
                columns: table => new
                {
                    noPhoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sourcePhoto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    idUtilisateur = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Cour",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idCour = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomCour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nomProf = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dureeSemaine = table.Column<int>(type: "int", nullable: false),
                    dureeJour = table.Column<int>(type: "int", nullable: false),
                    idClasse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cour__80B36504AB721395", x => x.idCour);
                    table.ForeignKey(
                        name: "FK__Cour__idClasse__6442E2C9",
                        column: x => x.idClasse,
                        principalSchema: "db_accessadmin",
                        principalTable: "Classe",
                        principalColumn: "idClasse",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eleve",
                schema: "db_accessadmin",
                columns: table => new
                {
                    idEleve = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noPv = table.Column<int>(type: "int", nullable: false),
                    idEtablissement = table.Column<int>(type: "int", nullable: false),
                    idClasse = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    prenom = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    date_naissance = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Eleve__0B66AF8E1E6E3F63", x => x.idEleve);
                    table.ForeignKey(
                        name: "FK__Eleve__idClasse__6166761E",
                        column: x => x.idClasse,
                        principalSchema: "db_accessadmin",
                        principalTable: "Classe",
                        principalColumn: "idClasse");
                    table.ForeignKey(
                        name: "FK__Eleve__idEtablis__607251E5",
                        column: x => x.idEtablissement,
                        principalSchema: "db_accessadmin",
                        principalTable: "Etablissement",
                        principalColumn: "idEtablissement");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classe_idEtablissement",
                schema: "db_accessadmin",
                table: "Classe",
                column: "idEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_Cour_idClasse",
                schema: "db_accessadmin",
                table: "Cour",
                column: "idClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Eleve_idClasse",
                schema: "db_accessadmin",
                table: "Eleve",
                column: "idClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Eleve_idEtablissement",
                schema: "db_accessadmin",
                table: "Eleve",
                column: "idEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_Message_idEtablissement",
                schema: "db_accessadmin",
                table: "Message",
                column: "idEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_Message_idUtilisateur",
                schema: "db_accessadmin",
                table: "Message",
                column: "idUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUtilisateur_idUtilisateur",
                schema: "db_accessadmin",
                table: "PhotoUtilisateur",
                column: "idUtilisateur");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cour",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Eleve",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "PhotoUtilisateur",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Classe",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Utilisateur",
                schema: "db_accessadmin");

            migrationBuilder.DropTable(
                name: "Etablissement",
                schema: "db_accessadmin");
        }
    }
}
