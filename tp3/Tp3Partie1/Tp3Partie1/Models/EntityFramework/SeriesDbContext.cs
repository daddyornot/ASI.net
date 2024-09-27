using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Tp3Partie1.Models.EntityFramework;

public partial class SeriesDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SeriesDbContext()
    {
    }

    public SeriesDbContext(DbContextOptions<SeriesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Serie> Series { get; set; }
    public virtual DbSet<Notation> Notations { get; set; }
    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public static readonly ILoggerFactory MyLoggerFactory =
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .EnableSensitiveDataLogging()
            .UseNpgsql("Server=localhost;port=5432;Database=postgres; uid=postgres; password=postgres;");
            // just for lazy loading
            // .UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.HasKey(e => e.SerieId).HasName("pk_ser");
            entity.HasIndex(e => e.Titre);
        });
        
        modelBuilder.Entity<Notation>(entity =>
        {
            entity.HasKey(e => new { e.SerieId, e.UtilisateurId }).HasName("pk_not");
            
            entity.HasOne(d => d.UtilisateurNotant).WithMany(p => p.NotesUtilisateur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_not_utl");
    
            entity.HasOne(d => d.SerieNotee).WithMany(p => p.NotesSerie)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_not_ser");
            
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.UtilisateurId).HasName("pk_utl");
            entity.HasIndex(e => e.Mail).IsUnique();

            entity.Property(e => e.Mobile).HasMaxLength(10).IsFixedLength();
            entity.Property(e => e.DateCreation).HasColumnType("date").HasDefaultValueSql("now()");
            entity.Property(e => e.Ville).HasDefaultValue("France");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
