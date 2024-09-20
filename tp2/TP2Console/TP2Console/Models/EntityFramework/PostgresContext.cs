using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP2Console.Models.EntityFramework;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avi> Avis { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=postgres; uid=postgres; password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avi>(entity =>
        {
            entity.HasKey(e => new { e.Idfilm, e.Idutilisateur }).HasName("pk_avis");

            entity.ToTable("avis");

            entity.Property(e => e.Idfilm).HasColumnName("idfilm");
            entity.Property(e => e.Idutilisateur).HasColumnName("idutilisateur");
            entity.Property(e => e.Commentaire)
                .HasMaxLength(1000)
                .HasColumnName("commentaire");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.IdfilmNavigation).WithMany(p => p.Avis)
                .HasForeignKey(d => d.Idfilm)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avis_film");

            entity.HasOne(d => d.IdutilisateurNavigation).WithMany(p => p.Avis)
                .HasForeignKey(d => d.Idutilisateur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avis_utilisateur");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorie");

            entity.ToTable("categorie");

            entity.Property(e => e.Idcategorie).HasColumnName("idcategorie");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Idfilm).HasName("pk_film");

            entity.ToTable("film");

            entity.Property(e => e.Idfilm).HasColumnName("idfilm");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Idcategorie).HasColumnName("idcategorie");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Films)
                .HasForeignKey(d => d.Idcategorie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_film_categorie");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Idutilisateur).HasName("pk_utilisateur");

            entity.ToTable("utilisateur");

            entity.Property(e => e.Idutilisateur).HasColumnName("idutilisateur");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Pwd)
                .HasMaxLength(64)
                .HasColumnName("pwd");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
