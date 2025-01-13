using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ColApp.Models;

namespace ColApp.Data
{
    public partial class BDEtabContext : DbContext
    {
        protected BDEtabContext()
        {
        }

        public BDEtabContext(DbContextOptions<BDEtabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Classe> Classes { get; set; } = null!;
        public virtual DbSet<Cour> Cours { get; set; } = null!;
        public virtual DbSet<Eleve> Eleves { get; set; } = null!;
        public virtual DbSet<Etablissement> Etablissements { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<PhotoUtilisateur> PhotoUtilisateurs { get; set; } = null!;
        public virtual DbSet<TentativesConnexion> TentativesConnexions { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("db_accessadmin");

            modelBuilder.Entity<Classe>(entity =>
            {
                entity.HasKey(e => e.IdClasse)
                    .HasName("PK__Classe__60FFF8016B1A79F7");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classe__idEtabli__5D95E53A");
            });

            modelBuilder.Entity<Cour>(entity =>
            {
                entity.HasKey(e => e.IdCour)
                    .HasName("PK__Cour__80B36504AB721395");

                entity.HasOne(d => d.IdClasseNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.IdClasse)
                    .HasConstraintName("FK__Cour__idClasse__6442E2C9");
            });

            modelBuilder.Entity<Eleve>(entity =>
            {
                entity.HasKey(e => e.IdEleve)
                    .HasName("PK__Eleve__0B66AF8E1E6E3F63");

                entity.HasOne(d => d.IdClasseNavigation)
                    .WithMany(p => p.Eleves)
                    .HasForeignKey(d => d.IdClasse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Eleve__idClasse__6166761E");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.Eleves)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Eleve__idEtablis__607251E5");
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.HasKey(e => e.IdEtablissement)
                    .HasName("PK__Etabliss__1E6C217E31806189");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("PK__Message__8D0E911DBD983FD1");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__idEtabl__6AEFE058");

                entity.HasOne(d => d.IdUtilisateurNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__idUtili__69FBBC1F");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.IdNotifications)
                    .HasName("PK__Notifica__430D4E1ACD5D51A9");
            });

            modelBuilder.Entity<PhotoUtilisateur>(entity =>
            {
                entity.HasKey(e => e.NoPhoto)
                    .HasName("PK__PhotoUti__746C0A8C4075D9BF");

                entity.HasOne(d => d.IdUtilisateurNavigation)
                    .WithMany(p => p.PhotoUtilisateurs)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhotoUtil__idUti__6DCC4D03");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur)
                    .HasName("PK__Utilisat__5366DB197F114924");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
