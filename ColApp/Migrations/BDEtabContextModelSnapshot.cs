﻿// <auto-generated />
using System;
using ColApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ColApp.Migrations
{
    [DbContext(typeof(BDEtabContext))]
    partial class BDEtabContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("db_accessadmin")
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ColApp.Models.Classe", b =>
                {
                    b.Property<int>("IdClasse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idClasse");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClasse"), 1L, 1);

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int")
                        .HasColumnName("idEtablissement");

                    b.Property<int>("NbEleves")
                        .HasColumnType("int")
                        .HasColumnName("nbEleves");

                    b.Property<string>("NomClasse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nomClasse");

                    b.HasKey("IdClasse")
                        .HasName("PK__Classe__60FFF8016B1A79F7");

                    b.HasIndex("IdEtablissement");

                    b.ToTable("Classe", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Cour", b =>
                {
                    b.Property<int>("IdCour")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idCour");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCour"), 1L, 1);

                    b.Property<int>("DureeJour")
                        .HasColumnType("int")
                        .HasColumnName("dureeJour");

                    b.Property<int>("DureeSemaine")
                        .HasColumnType("int")
                        .HasColumnName("dureeSemaine");

                    b.Property<int>("IdClasse")
                        .HasColumnType("int")
                        .HasColumnName("idClasse");

                    b.Property<string>("NomCour")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nomCour");

                    b.Property<string>("NomProf")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nomProf");

                    b.HasKey("IdCour")
                        .HasName("PK__Cour__80B36504AB721395");

                    b.HasIndex("IdClasse");

                    b.ToTable("Cour", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Eleve", b =>
                {
                    b.Property<int>("IdEleve")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEleve");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEleve"), 1L, 1);

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("date")
                        .HasColumnName("date_naissance");

                    b.Property<int>("IdClasse")
                        .HasColumnType("int")
                        .HasColumnName("idClasse");

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int")
                        .HasColumnName("idEtablissement");

                    b.Property<int>("NoPv")
                        .HasColumnType("int")
                        .HasColumnName("noPv");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nom");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("prenom");

                    b.HasKey("IdEleve")
                        .HasName("PK__Eleve__0B66AF8E1E6E3F63");

                    b.HasIndex("IdClasse");

                    b.HasIndex("IdEtablissement");

                    b.ToTable("Eleve", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Etablissement", b =>
                {
                    b.Property<int>("IdEtablissement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEtablissement");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEtablissement"), 1L, 1);

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("contact");

                    b.Property<DateTime>("DateRentre")
                        .HasColumnType("date")
                        .HasColumnName("dateRentre");

                    b.Property<int>("DelaiInscription")
                        .HasColumnType("int")
                        .HasColumnName("delaiInscription");

                    b.Property<string>("NomEtab")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nomEtab");

                    b.Property<string>("NomProviseur")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nomProviseur");

                    b.Property<string>("PrenomProviseur")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("prenomProviseur");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("province");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Secteur")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("secteur");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ville");

                    b.HasKey("IdEtablissement")
                        .HasName("PK__Etabliss__1E6C217E31806189");

                    b.ToTable("Etablissement", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Message", b =>
                {
                    b.Property<int>("IdMessage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idMessage");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMessage"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<int>("IdEtablissement")
                        .HasColumnType("int")
                        .HasColumnName("idEtablissement");

                    b.Property<int>("IdUtilisateur")
                        .HasColumnType("int")
                        .HasColumnName("idUtilisateur");

                    b.Property<string>("Message1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message");

                    b.HasKey("IdMessage")
                        .HasName("PK__Message__8D0E911DBD983FD1");

                    b.HasIndex("IdEtablissement");

                    b.HasIndex("IdUtilisateur");

                    b.ToTable("Message", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Notification", b =>
                {
                    b.Property<int>("IdNotifications")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idNotifications");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNotifications"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message");

                    b.Property<bool>("Vue")
                        .HasColumnType("bit")
                        .HasColumnName("vue");

                    b.HasKey("IdNotifications")
                        .HasName("PK__Notifica__430D4E1ACD5D51A9");

                    b.ToTable("Notifications", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.PhotoUtilisateur", b =>
                {
                    b.Property<int>("NoPhoto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("noPhoto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoPhoto"), 1L, 1);

                    b.Property<int>("IdUtilisateur")
                        .HasColumnType("int")
                        .HasColumnName("idUtilisateur");

                    b.Property<string>("SourcePhoto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("sourcePhoto");

                    b.HasKey("NoPhoto")
                        .HasName("PK__PhotoUti__746C0A8C4075D9BF");

                    b.HasIndex("IdUtilisateur");

                    b.ToTable("PhotoUtilisateur", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.SeSouvenirToken", b =>
                {
                    b.Property<DateTime>("DateExpiration")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.ToTable("SeSouvenirTokens", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Utilisateur", b =>
                {
                    b.Property<int>("IdUtilisateur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idUtilisateur");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtilisateur"), 1L, 1);

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("courriel");

                    b.Property<string>("DateNaissance")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("date_naissance");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("motDePasse");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nom");

                    b.Property<string>("PasswordResetToken")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("prenom");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("sel");

                    b.HasKey("IdUtilisateur")
                        .HasName("PK__Utilisat__5366DB197F114924");

                    b.ToTable("Utilisateur", "db_accessadmin");
                });

            modelBuilder.Entity("ColApp.Models.Classe", b =>
                {
                    b.HasOne("ColApp.Models.Etablissement", "IdEtablissementNavigation")
                        .WithMany("Classes")
                        .HasForeignKey("IdEtablissement")
                        .IsRequired()
                        .HasConstraintName("FK__Classe__idEtabli__5D95E53A");

                    b.Navigation("IdEtablissementNavigation");
                });

            modelBuilder.Entity("ColApp.Models.Cour", b =>
                {
                    b.HasOne("ColApp.Models.Classe", "IdClasseNavigation")
                        .WithMany("Cours")
                        .HasForeignKey("IdClasse")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Cour__idClasse__6442E2C9");

                    b.Navigation("IdClasseNavigation");
                });

            modelBuilder.Entity("ColApp.Models.Eleve", b =>
                {
                    b.HasOne("ColApp.Models.Classe", "IdClasseNavigation")
                        .WithMany("Eleves")
                        .HasForeignKey("IdClasse")
                        .IsRequired()
                        .HasConstraintName("FK__Eleve__idClasse__6166761E");

                    b.HasOne("ColApp.Models.Etablissement", "IdEtablissementNavigation")
                        .WithMany("Eleves")
                        .HasForeignKey("IdEtablissement")
                        .IsRequired()
                        .HasConstraintName("FK__Eleve__idEtablis__607251E5");

                    b.Navigation("IdClasseNavigation");

                    b.Navigation("IdEtablissementNavigation");
                });

            modelBuilder.Entity("ColApp.Models.Message", b =>
                {
                    b.HasOne("ColApp.Models.Etablissement", "IdEtablissementNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("IdEtablissement")
                        .IsRequired()
                        .HasConstraintName("FK__Message__idEtabl__6AEFE058");

                    b.HasOne("ColApp.Models.Utilisateur", "IdUtilisateurNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("IdUtilisateur")
                        .IsRequired()
                        .HasConstraintName("FK__Message__idUtili__69FBBC1F");

                    b.Navigation("IdEtablissementNavigation");

                    b.Navigation("IdUtilisateurNavigation");
                });

            modelBuilder.Entity("ColApp.Models.PhotoUtilisateur", b =>
                {
                    b.HasOne("ColApp.Models.Utilisateur", "IdUtilisateurNavigation")
                        .WithMany("PhotoUtilisateurs")
                        .HasForeignKey("IdUtilisateur")
                        .IsRequired()
                        .HasConstraintName("FK__PhotoUtil__idUti__6DCC4D03");

                    b.Navigation("IdUtilisateurNavigation");
                });

            modelBuilder.Entity("ColApp.Models.Classe", b =>
                {
                    b.Navigation("Cours");

                    b.Navigation("Eleves");
                });

            modelBuilder.Entity("ColApp.Models.Etablissement", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Eleves");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ColApp.Models.Utilisateur", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("PhotoUtilisateurs");
                });
#pragma warning restore 612, 618
        }
    }
}
